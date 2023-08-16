using System.Buffers.Binary;
using System.Numerics;
using RTree;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Systems.Script.Scene;
using static Weedwacker.GameServer.Systems.Script.Scene.SceneGroup;
using static Weedwacker.GameServer.Systems.World.Scene;

namespace Weedwacker.GameServer.Systems.World
{
    class SceneTreeCache
    {
        private readonly string _cacheDir;
        private readonly IDictionary<(uint, uint), IDictionary<CacheCell, HashSet<uint>>> _toPersist;
        private readonly IDictionary<(uint, uint), Dictionary<VisionLevelType, RTree<TreeEntry>>> _groupTrees;

        public SceneTreeCache(string cacheDir)
        {
            _cacheDir = cacheDir;

            _toPersist = new Dictionary<(uint, uint), IDictionary<CacheCell, HashSet<uint>>>();
            _groupTrees = new Dictionary<(uint, uint), Dictionary<VisionLevelType, RTree<TreeEntry>>>();
        }

        // TODO: Implement hashing for cache recreation on change
        public bool IsCached(uint sceneId, uint blockId)
        {
            return _groupTrees.ContainsKey((sceneId, blockId)) || File.Exists(GetGroupCacheFileInfo(sceneId, blockId).FullName);
        }

        // TODO: Implement hashing for cache recreation on change
        public IReadOnlyDictionary<VisionLevelType, RTree<TreeEntry>> LoadGroupTrees(uint sceneId, uint blockId)
        {
            // Return cached tree
            if (_groupTrees.TryGetValue((sceneId, blockId), out Dictionary<VisionLevelType, RTree<TreeEntry>>? trees))
                return trees;

            // Load tree from cache file
            FileInfo groupCacheFile = GetGroupCacheFileInfo(sceneId, blockId);
            if (File.Exists(groupCacheFile.FullName))
            {
                IDictionary<CacheCell, HashSet<uint>> loadedCells = ReadCacheFile(groupCacheFile.FullName);

                trees = CreateTreeLookup();
                foreach (CacheCell cell in loadedCells.Keys)
                {
                    foreach (uint groupId in loadedCells[cell])
                    {
                        VisionOption vision = VisionOptions[(int)cell.CellLevel];
                        AddToTree(trees[cell.CellLevel], groupId, new Vector3(cell.CellCoords.X * vision.Width + 1, 0, cell.CellCoords.Y * vision.Width + 1), cell.CellLevel);
                    }
                }

                return _groupTrees[(sceneId, blockId)] = trees;
            }

            // Otherwise, cannot load cached group positions
            throw new InvalidOperationException($"No cached tree for Scene {sceneId}, Block {blockId}. Use CacheGroupPositions(SceneGroup) instead.");
        }

        /// <summary>
        /// Cache all entity positions of a SceneGroup.
        /// </summary>
        /// <param name="group">The group to load entity positions from.</param>
        /// <remarks>Use <see cref="Persist"/> to persist entity positions to disc.</remarks>
        /// <remarks>Use <see cref="LoadGroupTrees"/> to retrieve the cached entity positions.</remarks>
        public void CacheGroupPositions(SceneGroup group)
        {
            uint groupId = group.GroupId;

            if (!_groupTrees.TryGetValue((group.SceneId, group.BlockId), out Dictionary<VisionLevelType, RTree<TreeEntry>>? trees))
                _groupTrees[(group.SceneId, group.BlockId)] = trees = CreateTreeLookup();

            // Collect entity positions
            if (!_toPersist.TryGetValue((group.SceneId, group.BlockId), out IDictionary<CacheCell, HashSet<uint>>? persistGroups))
                _toPersist[(group.SceneId, group.BlockId)] = persistGroups = new Dictionary<CacheCell, HashSet<uint>>();

            HashSet<VisionLevelType> visionLevels = new();

            foreach (KeyValuePair<uint, Monster> monster in group.monsters)
            {
                AddToCacheDictionary(persistGroups, groupId, monster.Value.pos, monster.Value.vision_level);
                AddToTree(trees[monster.Value.vision_level], groupId, monster.Value.pos, monster.Value.vision_level);

                visionLevels.Add(monster.Value.vision_level);
            }

            foreach (KeyValuePair<uint, Gadget> gadget in group.gadgets)
            {
                AddToCacheDictionary(persistGroups, groupId, gadget.Value.pos, gadget.Value.vision_level);
                AddToTree(trees[gadget.Value.vision_level], groupId, gadget.Value.pos, gadget.Value.vision_level);

                visionLevels.Add(gadget.Value.vision_level);
            }

            foreach (KeyValuePair<uint, Npc> npc in group.npcs)
            {
                AddToCacheDictionary(persistGroups, groupId, npc.Value.pos, npc.Value.vision_level);
                AddToTree(trees[npc.Value.vision_level], groupId, npc.Value.pos, npc.Value.vision_level);
            }

            foreach (Region region in group.regions)
            {
                AddToCacheDictionary(persistGroups, groupId, region.pos, VisionLevelType.VISION_LEVEL_NORMAL);
                AddToTree(trees[VisionLevelType.VISION_LEVEL_NORMAL], groupId, region.pos, VisionLevelType.VISION_LEVEL_NORMAL);
            }

            VisionLevelType groupVisionLevel = VisionLevelType.VISION_LEVEL_NORMAL;
            foreach (VisionLevelType visionLevel in visionLevels)
                if (VisionOptions[(int)groupVisionLevel].Width < VisionOptions[(int)visionLevel].Width)
                    groupVisionLevel = visionLevel;

            AddToCacheDictionary(persistGroups, groupId, group.Pos, groupVisionLevel);
            AddToTree(trees[groupVisionLevel], groupId, group.Pos, groupVisionLevel);
        }

        public void Persist(uint sceneId, uint blockId)
        {
            if (!_toPersist.ContainsKey((sceneId, blockId)))
                return;

            Directory.CreateDirectory(_cacheDir);

            WriteCacheFile(sceneId, blockId, GetGroupCacheFileInfo(sceneId, blockId).FullName);

            _toPersist.Remove((sceneId, blockId));
        }

        /// <summary>
        /// Get the file name of the tree cache.
        /// </summary>
        /// <param name="sceneId"></param>
        /// <param name="blockId"></param>
        /// <returns></returns>
        private FileInfo GetGroupCacheFileInfo(uint sceneId, uint blockId)
        {
            return new(Path.Combine(_cacheDir, $"scene{sceneId}_block{blockId}.bin"));
        }

        /// <summary>
        /// Loads a tree cache from a given file.
        /// </summary>
        /// <param name="cacheFile">The path to read the cache from.</param>
        /// <returns>The read tree cache for a scene and block.</returns>
        private IDictionary<CacheCell, HashSet<uint>> ReadCacheFile(string cacheFile)
        {
            Dictionary<CacheCell, HashSet<uint>> result = new();

            byte[] buffer = new byte[4];
            using FileStream fileStream = File.OpenRead(cacheFile);

            // Read sceneId
            fileStream.Read(buffer);

            // Read blockId
            fileStream.Read(buffer);

            while (fileStream.Position < fileStream.Length)
            {
                fileStream.Read(buffer);
                int cellX = BinaryPrimitives.ReadInt32LittleEndian(buffer);

                fileStream.Read(buffer);
                int cellY = BinaryPrimitives.ReadInt32LittleEndian(buffer);

                fileStream.Read(buffer, 0, 1);
                VisionLevelType groupLevel = (VisionLevelType)buffer[0];

                fileStream.Read(buffer, 0, 2);
                int groupCount = BinaryPrimitives.ReadInt16LittleEndian(buffer);

                HashSet<uint> groupIds = new();
                for (int i = 0; i < groupCount; i++)
                {
                    fileStream.Read(buffer);
                    uint id = BinaryPrimitives.ReadUInt32LittleEndian(buffer);

                    groupIds.Add(id);
                }

                result.Add(new(new CellCoordinate(cellX, cellY), groupLevel), groupIds);
            }

            return result;
        }

        /// <summary>
        /// Write a tree cache to a given file.
        /// </summary>
        /// <param name="sceneId">The scene of the cache to persist.</param>
        /// <param name="blockId">The block in the scene to persist.</param>
        /// <param name="cacheFile">The path to persist the cache at.</param>
        private void WriteCacheFile(uint sceneId, uint blockId, string cacheFile)
        {
            byte[] buffer = new byte[4];
            using FileStream fileStream = File.OpenWrite(cacheFile);

            BinaryPrimitives.WriteUInt32LittleEndian(buffer, sceneId);
            fileStream.Write(buffer);

            BinaryPrimitives.WriteUInt32LittleEndian(buffer, blockId);
            fileStream.Write(buffer);

            IDictionary<CacheCell, HashSet<uint>> persistGroups = _toPersist[(sceneId, blockId)];
            foreach (CacheCell cell in persistGroups.Keys)
            {
                HashSet<uint> groupIds = persistGroups[cell];

                BinaryPrimitives.WriteInt32LittleEndian(buffer, cell.CellCoords.X);
                fileStream.Write(buffer);

                BinaryPrimitives.WriteInt32LittleEndian(buffer, cell.CellCoords.Y);
                fileStream.Write(buffer);

                fileStream.WriteByte((byte)cell.CellLevel);

                BinaryPrimitives.WriteInt16LittleEndian(buffer, (short)groupIds.Count);
                fileStream.Write(buffer, 0, 2);

                foreach (uint groupId in groupIds)
                {
                    BinaryPrimitives.WriteUInt32LittleEndian(buffer, groupId);
                    fileStream.Write(buffer);
                }
            }
        }

        /// <summary>
        /// Adds the <paramref name="groupId"/> to the cell in the cache denoted by <paramref name="pos"/> to persist later on.
        /// </summary>
        /// <param name="groupIds">The cache to add the <paramref name="groupId"/> to.</param>
        /// <param name="groupId">The groupId to add to the cell in the cache.</param>
        /// <param name="pos">The position to calculate the cell coordinate in the cache from.</param>
        /// <param name="visionLevel">The vision level to determine the correct cell dimensions.</param>
        private void AddToCacheDictionary(IDictionary<CacheCell, HashSet<uint>> groupIds, uint groupId, Vector3 pos, VisionLevelType visionLevel)
        {
            VisionOption visionOption = VisionOptions[(int)visionLevel];

            // HINT: Precision loss by int cast is intended to have cell coordinates as full integers
            int cellX = (int)(pos.X / visionOption.Width);
            int cellY = (int)(pos.Z / visionOption.Width);
            CacheCell cell = new(new CellCoordinate(cellX, cellY), visionLevel);

            if (!groupIds.TryGetValue(cell, out HashSet<uint>? ids))
                groupIds[cell] = ids = new HashSet<uint>();

            ids.Add(groupId);
        }

        /// <summary>
        /// Adds the <paramref name="groupId"/> to the cell in the RTree denoted by <paramref name="pos"/>.
        /// </summary>
        /// <param name="tree">The RTree to add the <paramref name="groupId"/> to.</param>
        /// <param name="groupId">The groupId to add to the cell in the RTree.</param>
        /// <param name="pos">The position to calculate the cell coordinate in the tree from.</param>
        /// <param name="visionLevel">The vision level to determine the correct cell dimensions.</param>
        private void AddToTree(RTree<TreeEntry> tree, uint groupId, Vector3 pos, VisionLevelType visionLevel)
        {
            VisionOption visionOption = VisionOptions[(int)visionLevel];

            // HINT: Precision loss by int cast is intended to have cell coordinates as full integers
            int cellX = (int)(pos.X / visionOption.Width);
            int cellY = (int)(pos.Z / visionOption.Width);

            tree.Add(new Rectangle(new float[] { cellX * visionOption.Width, 0, cellY * visionOption.Width }, new float[] { (cellX + 1) * visionOption.Width, 0, (cellY + 1) * visionOption.Width }), new(groupId));
        }

        private Dictionary<VisionLevelType, RTree<TreeEntry>> CreateTreeLookup()
        {
            Dictionary<VisionLevelType, RTree<TreeEntry>> trees = new();
            foreach (VisionOption visionOption in VisionOptions)
                trees[visionOption.Level] = new RTree<TreeEntry>();

            return trees;
        }

        record CellCoordinate(int X, int Y);
        record CacheCell(CellCoordinate CellCoords, VisionLevelType CellLevel);

        public class TreeEntry
        {
            public uint GroupId { get; }

            public TreeEntry(uint groupId)
            {
                GroupId = groupId;
            }
        }
    }
}
