using System.Collections;
using System.Numerics;
using NLua;
using Weedwacker.GameServer.Data;
using Weedwacker.Shared.Enums;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Systems.Script.Scene;

internal class SceneBlock
{
    private Lua LuaState;
    public readonly uint SceneId;
    public readonly uint BlockId;

    private SortedList<uint, SceneGroupInfo> groups = new(); // index
    private SortedList<uint, SceneGroup> GroupsInfo = new(); // SceneGroupInfo::id

    private SceneBlock(Lua lua, uint sceneId, uint blockId)
    {
        LuaState = lua;
        SceneId = sceneId;
        BlockId = blockId;
    }

    public SceneGroup? GetSceneGroup(uint groupId)
    {
        if (GroupsInfo.TryGetValue(groupId, out SceneGroup? group))
            return group;

        group = SceneGroup.Create(LuaState, SceneId, BlockId, groupId, groups[groupId].pos, GameServer.Configuration.structure.Scripts);
        if (group == null)
            return null;

        return GroupsInfo[groupId] = group;
    }

    internal static SceneBlock? Create(Lua lua, uint sceneId, uint blockId, string scriptPath)
    {
        SceneBlock block = new(lua, sceneId, blockId);
        return block.Initialize(scriptPath);
    }

    private SceneBlock? Initialize(string scriptPath)
    {
        FileInfo sceneBlockInfo = new(Path.Combine(scriptPath, "scene", $"{SceneId}", $"scene{SceneId}_block{BlockId}.lua"));
        string blockScript = sceneBlockInfo.FullName.Replace(@"\", @"\\");

        if (!File.Exists(sceneBlockInfo.FullName))
        {
#if DEBUG
            if (GameServer.Configuration.Server.LogOptions.LogScenes is SceneDebugMode.DEBUG or SceneDebugMode.WARN or SceneDebugMode.ERROR or SceneDebugMode.ALL)
                Logger.DebugWriteLine($"Could not find file {sceneBlockInfo.FullName}");
#endif
            return null;
        }

        try
        {
            LuaState.DoString($$"""
                _SCENE_BLOCK{{BlockId}} = {}
                loadfile("{{blockScript}}", "bt", _SCENE_BLOCK{{BlockId}})()
                """);
        }
        catch (Exception e)
        {
            if (GameServer.Configuration.Server.LogOptions.LogScenes is SceneDebugMode.DEBUG or SceneDebugMode.WARN or SceneDebugMode.ERROR or SceneDebugMode.ALL)
                Logger.WriteErrorLine($"Failed to load {sceneBlockInfo.Name}", e);
            return null;
        }

        LuaTable? groupTable = (LuaTable?)LuaState[$"_SCENE_BLOCK{BlockId}.{nameof(groups)}"];
        if (groupTable != null)
        {
            Dictionary<uint, LuaTable> groupDict = LuaState.GetTableDict(groupTable).ToDictionary(w => (uint)(long)w.Key, w => (LuaTable)w.Value);
            foreach ((_, LuaTable group) in groupDict)
            {
                uint groupId = (uint)(long)group["id"];
                groups.Add(groupId, new SceneGroupInfo(LuaState, group));
            }

            if (!GameServer.TreeCache.IsCached(SceneId, BlockId))
            {
                foreach (SceneGroupInfo groupInfo in groups.Values)
                {
                    if (groupInfo.dynamic_load)
                        continue;

                    uint groupId = groupInfo.id;

                    SceneGroup? group = SceneGroup.Create(LuaState, SceneId, BlockId, groupId, groupInfo.pos, scriptPath);
                    if (group == null)
                        continue;

                    GameServer.TreeCache.CacheGroupPositions(group);

                    GroupsInfo.Add(groupId, group);
                }

                GameServer.TreeCache.Persist(SceneId, BlockId);
            }
        }

        return this;
    }

    public class SceneGroupInfo
    {
        private readonly LuaTable Table;

        public uint id => (uint)(long)Table[$"{nameof(id)}"];
        public uint area => (uint?)(long?)Table[$"{nameof(area)}"] ?? 0;
        public Vector3 pos => new((float?)(double?)Table[$"{nameof(pos)}.x"] ?? 0, (float?)(double?)Table[$"{nameof(pos)}.y"] ?? 0, (float?)(double?)Table[$"{nameof(pos)}.z"] ?? 0);
        public List<uint>? related_level_tag_series_list;
        public List<Dictionary<string, uint>>? life_cycle; // list of pairs "from": number, and "to": number
        public bool dynamic_load => (bool?)Table[$"{nameof(dynamic_load)}"] ?? false;
        public uint load_strategy => (uint?)(long?)Table[$"{nameof(load_strategy)}"] ?? 0;
        public uint refresh_id => (uint?)(long?)Table[$"{nameof(refresh_id)}"] ?? 0;
        public bool limit_drop => (bool?)Table[$"{nameof(limit_drop)}"] ?? false;
        public IsReplacable? is_replaceable;
        public bool force_unload_nodelay => (bool?)Table[$"{nameof(force_unload_nodelay)}"] ?? false;
        public uint vision_type => (uint?)(long?)Table[$"{nameof(vision_type)}"] ?? 0;
        public Business? business;
        public bool is_ignore_world_level_revise => (bool?)Table[$"{nameof(is_ignore_world_level_revise)}"] ?? false;
        public uint activity_revise_level_grow_id => (uint?)(long?)Table[$"{nameof(activity_revise_level_grow_id)}"] ?? 0;

        public class IsReplacable
        {
            public bool value;
            public uint version;
            public bool new_bin_only;
            public IsReplacable(LuaTable table)
            {
                value = (bool?)table[$"{nameof(value)}"] ?? false;
                version = (uint?)(long?)table[$"{nameof(version)}"] ?? 0;
                new_bin_only = (bool?)table[$"{nameof(new_bin_only)}"] ?? false;
            }
        }
        public class Business
        {
            public readonly uint type;

            public Business(LuaTable table)
            {
                type = (uint?)(long?)table[$"{nameof(type)}"] ?? 0;
            }
        }

        public SceneGroupInfo(Lua luaState, LuaTable table)
        {
            Table = table;
            is_replaceable = new IsReplacable(table);
            business = new Business(table);
            if (table[$"{nameof(related_level_tag_series_list)}"] != null)
            {
                List<object>? list = new((IEnumerable<object>)(table[$"{nameof(related_level_tag_series_list)}"] as LuaTable).Values);
                related_level_tag_series_list = new List<uint>(list.Select(w => (uint)(long)w));
            }
            if (table[$"{nameof(life_cycle)}"] != null)
            {
                life_cycle = new List<Dictionary<string, uint>>();
                ICollection? list = (table[$"{nameof(life_cycle)}"] as LuaTable).Values;
                foreach (object? dict in list)
                {
                    Dictionary<string, uint>? v1 = luaState.GetTableDict(dict as LuaTable).ToDictionary(w => (string)w.Key, w => (uint)(long)w.Value);
                    life_cycle.Add(v1);
                }
            }
        }
    }
}
