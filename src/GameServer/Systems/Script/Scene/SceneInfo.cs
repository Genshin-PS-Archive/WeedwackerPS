using System.Numerics;
using NLua;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.Shared.Enums;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Systems.Script.Scene;

internal class SceneInfo
{
    private Lua LuaState;
    public readonly uint SceneId;
    public SceneConfig? scene_config;

    public SortedList<int, uint>? blocks; // <index ,blockIds>
    public SortedList<int, Rectangle>? block_rects = new(); // <index, rectangle>
    public DummyPoints? dummy_points; // load dummy points from Scene<sceneId>_dummy_points.lua
    public LuaTable routes_config;// => LuaState.GetTable("routes_config"); // load routes from ???

    public SortedList<uint, SceneBlock>? BlocksInfo; // blockId

    private SceneInfo(Lua lua, uint sceneId)
    {
        LuaState = lua;
        LuaState = lua;
        SceneId = sceneId;
    }

    public static SceneInfo? Create(uint sceneId, string scriptPath)
    {
        Lua lua = CreateLuaState();

        SceneInfo scene = new(lua, sceneId);
        return scene.Initialize(scriptPath);
    }

    private SceneInfo? Initialize(string scriptPath)
    {
        FileInfo sceneInfo = new(Path.Combine(scriptPath, "scene", $"{SceneId}", $"scene{SceneId}.lua"));
        string sceneScript = sceneInfo.FullName.Replace(@"\", @"\\");

        if (!File.Exists(sceneInfo.FullName))
        {
            if (GameServer.Configuration.Server.LogOptions.LogScenes is SceneDebugMode.ERROR or SceneDebugMode.ALL)
                Logger.WriteErrorLine($"Could not find file {sceneInfo.FullName}");
            return null;
        }

        try
        {
            LuaState.DoString($$"""
                _SCENE{{SceneId}} = {}
                loadfile("{{sceneScript}}", "bt", _SCENE{{SceneId}})()
                """);
        }
        catch(Exception e)
        {
            if (GameServer.Configuration.Server.LogOptions.LogScenes is SceneDebugMode.ERROR or SceneDebugMode.ALL)
                Logger.WriteErrorLine($"Failed to load {sceneInfo.Name}", e);
            return null;
        }

        LuaTable? blockTable = (LuaTable?)LuaState[$"_SCENE{SceneId}.{nameof(blocks)}"];
        if (blockTable != null)
        {
            blocks = new SortedList<int, uint>(LuaState.GetTableDict(blockTable).ToDictionary(w => (int)(long)w.Key, w => (uint)(long)w.Value));

            BlocksInfo = new();
            foreach (uint blockId in blocks.Values)
            {
                SceneBlock? block = SceneBlock.Create(LuaState, SceneId, blockId, scriptPath);
                if (block != null)
                    BlocksInfo.Add(blockId, block);
            }
        }

        LuaTable? sceneConfigTable = (LuaTable?)LuaState[$"_SCENE{SceneId}.{nameof(scene_config)}"];
        if (sceneConfigTable != null)
            scene_config = new SceneConfig(sceneConfigTable);

        LuaTable? blockRects = (LuaTable?)LuaState[$"_SCENE{SceneId}.{nameof(block_rects)}"];
        if (blockRects != null)
            block_rects = new SortedList<int, Rectangle>(LuaState.GetTableDict(blockRects).ToDictionary(w => (int)(long)w.Key, w => new Rectangle(w.Value as LuaTable)));

        LuaTable? dummyPoints = (LuaTable?)LuaState[$"_SCENE{SceneId}.{nameof(dummy_points)}"];
        if (dummyPoints != null)
            dummy_points = DummyPoints.Create(LuaState, SceneId, scriptPath);

        return this;
    }

    private static Lua CreateLuaState()
    {
        Lua lua = new Lua();

        lua.LoadCLRPackage();

        // Register necessary enums for scripts
        LuaRegistrationHelper.Enumeration<AnimatorParamType>(lua);
        LuaRegistrationHelper.Enumeration<ControlPartForwardBy>(lua);
        LuaRegistrationHelper.Enumeration<ControlPartRotateBy>(lua);
        LuaRegistrationHelper.Enumeration<ControlPartDoOnUnEnabled>(lua);
        LuaRegistrationHelper.Enumeration<DropElemControlType>(lua);
        LuaRegistrationHelper.Enumeration<EndureType>(lua);
        LuaRegistrationHelper.Enumeration<EntityType>(lua);
        LuaRegistrationHelper.Enumeration<EventType>(lua);
        LuaRegistrationHelper.Enumeration<GadgetState>(lua);
        LuaRegistrationHelper.Enumeration<GadgetType>(lua);
        LuaRegistrationHelper.Enumeration<GearType>(lua);
        LuaRegistrationHelper.Enumeration<GroupKillPolicy>(lua);
        LuaRegistrationHelper.Enumeration<ParentQuestState>(lua);
        LuaRegistrationHelper.Enumeration<PlatformRotType>(lua);
        LuaRegistrationHelper.Enumeration<PickType>(lua);
        LuaRegistrationHelper.Enumeration<QuestCondType>(lua);
        LuaRegistrationHelper.Enumeration<QuestContentType>(lua);
        LuaRegistrationHelper.Enumeration<QuestExecType>(lua);
        LuaRegistrationHelper.Enumeration<QuestGuideAuto>(lua);
        LuaRegistrationHelper.Enumeration<QuestGuideLayer>(lua);
        LuaRegistrationHelper.Enumeration<QuestGuideStyle>(lua);
        LuaRegistrationHelper.Enumeration<QuestGuideType>(lua);
        LuaRegistrationHelper.Enumeration<QuestShowType>(lua);
        LuaRegistrationHelper.Enumeration<QuestState>(lua);
        LuaRegistrationHelper.Enumeration<QuestType>(lua);
        LuaRegistrationHelper.Enumeration<RandomQuestFilterType>(lua);
        LuaRegistrationHelper.Enumeration<RegionShape>(lua);
        LuaRegistrationHelper.Enumeration<ShowQuestGuideType>(lua);
        LuaRegistrationHelper.Enumeration<TalkBeginWay>(lua);
        LuaRegistrationHelper.Enumeration<TalkHeroType>(lua);
        LuaRegistrationHelper.Enumeration<TalkRoleType>(lua);
        LuaRegistrationHelper.Enumeration<TalkShowType>(lua);
        LuaRegistrationHelper.Enumeration<VisionLevelType>(lua);

        // Register override for module searches, that uses / as the directory separator instead
        lua.DoString("""
                local function searcher(module_name)
                    -- Use "/" instead of "." as directory separator
                    local path, err = package.searchpath(module_name, package.path, "/")
                    if path then
                        return assert(loadfile(path))
                    end
                    return err
                end
                table.insert(package.searchers, searcher)
                """);

        return lua;
    }

    public class SceneConfig
    {
        private readonly LuaTable Table;
        // ONLY X,Z
        public readonly Vector3 begin_pos;
        // ONLY X,Z
        public readonly Vector3 size;
        public readonly Vector3 born_pos;
        public readonly Vector3 born_rot;
        public readonly float? die_y;
        public RoomInfo room_safe_pos;
        // ONLY X,Z
        public Vector3 vision_anchor => new((float?)(double?)Table[$"{nameof(vision_anchor)}.x"] ?? 0, default, (float?)(double?)Table[$"{nameof(vision_anchor)}.z"] ?? 0);

        public SceneConfig(LuaTable table)
        {
            Table = table;
            if (table[$"{nameof(room_safe_pos)}"] is LuaTable roomTable)
                room_safe_pos = new RoomInfo(roomTable);
            begin_pos = new((float?)(double?)Table[$"{nameof(begin_pos)}.x"] ?? 0, default, (float?)(double?)Table[$"{nameof(begin_pos)}.z"] ?? 0);
            size = new((float?)(double?)Table[$"{nameof(size)}.x"] ?? 0, default, (float?)(double?)Table[$"{nameof(begin_pos)}.z"] ?? 0);
            born_pos = new((float?)(double?)Table[$"{nameof(born_pos)}.x"] ?? 0, (float?)(double?)Table[$"{nameof(born_pos)}.y"] ?? 0, (float?)(double?)Table[$"{nameof(born_pos)}.z"] ?? 0);
            born_rot = new((float?)(double?)Table[$"{nameof(born_rot)}.x"] ?? 0, (float?)(double?)Table[$"{nameof(born_rot)}.y"] ?? 0, (float?)(double?)Table[$"{nameof(born_rot)}.z"] ?? 0);
            die_y = (int?)(long?)Table[$"die_y"];
        }
    }

    public class RoomInfo
    {
        private readonly LuaTable Table;
        public readonly int? scene_id;
        public readonly Vector3 safe_pos;
        public readonly Vector3 safe_rot;

        public RoomInfo(LuaTable table)
        {
            Table = table;
            scene_id = (int?)(long?)Table[$"{nameof(scene_id)}"] ?? 0;
            safe_pos = new((float?)(double?)Table[$"{nameof(safe_pos)}.x"] ?? 0, (float?)(double?)Table[$"{nameof(safe_pos)}.y"] ?? 0, (float?)(double?)Table[$"{nameof(safe_pos)}.z"] ?? 0);
            safe_rot = new((float?)(double?)Table[$"{nameof(safe_rot)}.x"] ?? 0, (float?)(double?)Table[$"{nameof(safe_rot)}.y"] ?? 0, (float?)(double?)Table[$"{nameof(safe_rot)}.z"] ?? 0);
        }
    }

    public class Rectangle
    {
        private readonly LuaTable Table;
        // ONLY X,Z
        public readonly Vector3 min;
        // ONLY X,Z
        public readonly Vector3 max;

        public Rectangle(LuaTable table)
        {
            Table = table;
            min = new((float?)(double?)Table[$"{nameof(min)}.x"] ?? 0, default, (float?)(double?)Table[$"{nameof(min)}.z"] ?? 0);
            max = new((float?)(double?)Table[$"{nameof(max)}.x"] ?? 0, default, (float?)(double?)Table[$"{nameof(max)}.z"] ?? 0);
        }
    }
}
