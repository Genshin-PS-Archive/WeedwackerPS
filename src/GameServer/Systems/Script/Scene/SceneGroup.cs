using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;
using NLua;
using Weedwacker.GameServer.Data;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Enums;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Systems.Script.Scene;

internal partial class SceneGroup
{
    private readonly Lua LuaState;
    public readonly uint SceneId;
    public readonly uint BlockId;
    public readonly uint GroupId;
    public readonly Vector3 Pos;

    public SortedDictionary<uint, Monster>? monsters;
    public SortedList<uint, Npc>? npcs;
    public Dictionary<uint, Gadget>? gadgets; // config_id
    public List<Region>? regions;
    public List<Trigger>? triggers;
    public List<Variable>? variables;
    public SortedList<uint, Suite> suites;
    public InitConfig init_config;

    private SceneGroup(Lua luaState, uint sceneId, uint blockId, uint groupId, Vector3 pos)
    {
        LuaState = luaState;
        SceneId = sceneId;
        BlockId = blockId;
        GroupId = groupId;
        Pos = pos;
    }

    internal static SceneGroup? Create(Lua luaState, uint sceneId, uint blockId, uint id, Vector3 pos, string scriptPath)
    {
        SceneGroup group = new(luaState, sceneId, blockId, id, pos);
        return group.Initialize(scriptPath);
    }

    private SceneGroup? Initialize(string scriptPath)
    {
        FileInfo groupInfo = new(Path.Combine(scriptPath, "scene", $"{SceneId}", $"scene{SceneId}_group{GroupId}.lua"));
        if (!File.Exists(groupInfo.FullName))
        {
#if DEBUG
            if (GameServer.Configuration.Server.LogOptions.LogScenes is SceneDebugMode.DEBUG or SceneDebugMode.WARN or SceneDebugMode.ERROR or SceneDebugMode.ALL)
                Logger.DebugWriteLine($"Could not find file {groupInfo.FullName}");
#endif
            return null;
        }

        string script = groupInfo.FullName.Replace(@"\", @"\\");
        string common = Path.Combine(scriptPath, "common").Replace(@"\", @"\\");

        LuaState.DoString("""
            function sandbox_require()
                -- Replace require() with one that doesn't pollute _G, required
                -- for neat sandboxing of modules
                local _realG = _G;
                local _real_require = require;
                local getfenv = getfenv or function (f)
                    -- FIXME: This is a hack to replace getfenv() in Lua 5.2
                    local name, env = debug.getupvalue(debug.getinfo(f or 1).func, 1);
                    if name == "_ENV" then
                        return env;
                    end
                end
                function require(...)
                    local curr_env = getfenv(2);
                    local curr_env_mt = getmetatable(curr_env);
                    local _realG_mt = getmetatable(_realG);
                    if curr_env_mt and curr_env_mt.__index and not curr_env_mt.__newindex and _realG_mt then
                        local old_newindex, old_index;
                        old_newindex, _realG_mt.__newindex = _realG_mt.__newindex, curr_env;
                        old_index, _realG_mt.__index = _realG_mt.__index, function (_G, k)
                            return rawget(curr_env, k);
                        end;
                        local ret = _real_require(...);
                        _realG_mt.__newindex = old_newindex;
                        _realG_mt.__index = old_index;
                        return ret;
                    end
                    return _real_require(...);
                end
            end
            """);

        LuaState.DoString(@$"_SCENE_GROUP{GroupId} = {{ EventType = _G.EventType; GadgetState = _G.GadgetState; RegionShape = _G.RegionShape; QuestState = _G.QuestState; EntityType = _G.EntityType; VisionLevelType = _G.VisionLevelType; ScriptLib = _G.ScriptLib; table = _G.table; require = sandbox_require; tostring = _G.tostring; }}");
        LuaState.DoString($"package.path = '{common}/?.lua;' .. package.path");

        try
        {
            LuaState.DoString($$"""
                success, loadGroup{{GroupId}} = pcall(loadfile, "{{script}}", "bt", _SCENE_GROUP{{GroupId}})
                loadGroup{{GroupId}}()
                """);
        }
        catch (Exception e)
        {
            if (GameServer.Configuration.Server.LogOptions.LogScenes is SceneDebugMode.DEBUG or SceneDebugMode.WARN or SceneDebugMode.ERROR or SceneDebugMode.ALL)
                Logger.WriteErrorLine($"Failed to load scene group {GroupId}", e);
            return null;
        }

        if (LuaState[$"_SCENE_GROUP{GroupId}.{nameof(init_config)}"] != null)
            init_config = new InitConfig(LuaState[$"_SCENE_GROUP{GroupId}.{nameof(init_config)}"] as LuaTable);
        if (LuaState[$"_SCENE_GROUP{GroupId}.{nameof(monsters)}"] != null)
        {
            Dictionary<object, object>? table = LuaState.GetTableDict(LuaState.GetTable($"_SCENE_GROUP{GroupId}.{nameof(monsters)}"));
            monsters = new SortedDictionary<uint, Monster>(table.ToDictionary(w => (uint)(long)(w.Value as LuaTable)["config_id"], w => new Monster(w.Value as LuaTable, this)));
        }
        if (LuaState[$"_SCENE_GROUP{GroupId}.{nameof(npcs)}"] != null)
            npcs = new SortedList<uint, Npc>(LuaState.GetTableDict(LuaState.GetTable($"_SCENE_GROUP{GroupId}.{nameof(npcs)}")).ToDictionary(w => (uint)(long)w.Key, w => new Npc(w.Value as LuaTable, this)));
        if (LuaState[$"_SCENE_GROUP{GroupId}.{nameof(gadgets)}"] != null)
            gadgets = LuaState.GetTableDict(LuaState.GetTable($"_SCENE_GROUP{GroupId}.{nameof(gadgets)}")).ToDictionary(w => (uint)(long)w.Key, w => new Gadget(w.Value as LuaTable, this)).ToDictionary(w => w.Value.config_id, w => w.Value);
        if (LuaState[$"_SCENE_GROUP{GroupId}.{nameof(regions)}"] != null)
            regions = new List<Region>(LuaState.GetTableDict(LuaState.GetTable($"_SCENE_GROUP{GroupId}.{nameof(regions)}")).Values.Select(w => new Region(w as LuaTable)));
        if (LuaState[$"_SCENE_GROUP{GroupId}.{nameof(triggers)}"] != null)
            triggers = new List<Trigger>(LuaState.GetTableDict(LuaState.GetTable($"_SCENE_GROUP{GroupId}.{nameof(triggers)}")).Values.Select(w => new Trigger(w as LuaTable)));
        if (LuaState[$"_SCENE_GROUP{GroupId}.{nameof(variables)}"] != null)
            variables = new List<Variable>(LuaState.GetTableDict(LuaState.GetTable($"_SCENE_GROUP{GroupId}.{nameof(variables)}")).Values.Select(w => new Variable(w as LuaTable)));

        if (LuaState[$"_SCENE_GROUP{GroupId}.{nameof(suites)}"] != null)
            suites = new SortedList<uint, Suite>(LuaState.GetTableDict(LuaState.GetTable($"_SCENE_GROUP{GroupId}.{nameof(suites)}")).ToDictionary(w => (uint)(long)w.Key, w => new Suite(LuaState, w.Value as LuaTable)));

        return this;
    }

    internal async void OnInitAsync(World.Scene scene)
    {
        if (init_config == null)
            return;
        try
        {
            List<SceneEntity>? entities = new();
            Suite init_suite = suites[init_config.suite];
            if (init_suite.monsters != null && init_suite.monsters.Any())
            {

                foreach (uint monsterIndex in init_suite.monsters)
                {
                    Monster monster = monsters[monsterIndex];
                    MonsterEntity? entity = await MonsterEntity.CreateAsync(scene, GameData.MonsterDataMap[monster.monster_id], monster.level, monster, BlockId, GroupId);
                    entities.Add(entity);
                }
            }
            if (init_suite.gadgets != null && init_suite.gadgets.Any())
            {
                foreach (uint configId in init_suite.gadgets)
                {
                    BaseGadgetEntity? gadget = await BaseGadgetEntity.CreateGadgetAsync(scene, scene.World.Host, gadgets[configId]);
                    if (gadget != null) entities.Add(gadget);
                }
            }
            if (entities.Any())
                await scene.AddEntitiesAsync(entities);

#if DEBUG
            if (GameServer.Configuration.Server.LogOptions.LogScenes is SceneDebugMode.DEBUG or SceneDebugMode.ALL)
                Logger.DebugWriteLine($"Loaded SceneGroup{GroupId} init_config");
#endif
        }
        catch (Exception e)
        {
            if (GameServer.Configuration.Server.LogOptions.LogScenes is SceneDebugMode.DEBUG or SceneDebugMode.WARN or SceneDebugMode.ERROR or SceneDebugMode.ALL)
                Logger.WriteErrorLine($"Error loading SceneGroup{GroupId}", e);
        }
    }

    internal async void OnUnload(World.Scene scene)
    {
        IEnumerable<SceneEntity> toUnload = scene.ScriptEntities.Values.Where(w => w.GroupId == GroupId).Select(w => w as SceneEntity);
        await scene.RemoveEntitiesAsync(toUnload, Shared.Network.Proto.VisionType.Remove);

#if DEBUG
        if (GameServer.Configuration.Server.LogOptions.LogScenes is SceneDebugMode.DEBUG or SceneDebugMode.ALL)
            Logger.DebugWriteLine($"Unloaded SceneGroup{GroupId} entities");
#endif
    }

    public class InitConfig
    {
        public readonly uint suite;
        public readonly uint end_suite;
        public readonly bool rand_suite;

        public InitConfig(LuaTable table)
        {
            suite = (uint?)(long?)table[$"{nameof(suite)}"] ?? 0;
            end_suite = (uint?)(long?)table[$"{nameof(end_suite)}"] ?? 0;
            rand_suite = (bool?)table[$"{nameof(rand_suite)}"] ?? false;
        }
    }

    public class Suite
    {
        public readonly List<uint>? monsters; // config_id
        public readonly List<uint>? npcs; // config_id
        public readonly List<uint>? gadgets; // config_id
        public readonly List<uint>? regions; // config_id
        public readonly List<string>? triggers; // substring of trigger's name/action
        public readonly uint rand_weight;
        public readonly bool ban_refresh;

        public Suite(Lua luastate, LuaTable table)
        {
            rand_weight = (uint?)(long?)table[$"{nameof(rand_weight)}"] ?? 0;
            ban_refresh = (bool?)table[$"{nameof(ban_refresh)}"] ?? false;
            if (table[$"{nameof(monsters)}"] != null)
                monsters = new List<uint>(luastate.GetTableDict(table[$"{nameof(monsters)}"] as LuaTable).Values.Select(w => (uint)(long)w));
            if (table[$"{nameof(npcs)}"] != null)
                npcs = new List<uint>(luastate.GetTableDict(table[$"{nameof(npcs)}"] as LuaTable).Values.Select(w => (uint)(long)w));
            if (table[$"{nameof(gadgets)}"] != null)
                gadgets = new List<uint>(luastate.GetTableDict(table[$"{nameof(gadgets)}"] as LuaTable).Values.Select(w => (uint)(long)w));
            if (table[$"{nameof(regions)}"] != null)
                regions = new List<uint>(luastate.GetTableDict(table[$"{nameof(regions)}"] as LuaTable).Values.Select(w => (uint)(long)w));
            if (table[$"{nameof(monsters)}"] != null)
                triggers = new List<string>(luastate.GetTableDict(table[$"{nameof(triggers)}"] as LuaTable).Values.Select(w => (string)w));
        }
    }

    public abstract class SpawnInfo
    {
        public readonly uint config_id;
        public readonly uint area_id;
        public readonly Vector3 pos;
        public readonly Vector3 rot;
        public readonly VisionLevelType vision_level;

        //Meta fields
        public readonly uint SceneId;
        public readonly uint BlockId;
        public readonly uint GroupId;

        protected SpawnInfo(LuaTable table, SceneGroup group)
        {
            config_id = (uint?)(long?)table[$"{nameof(config_id)}"] ?? 0;
            area_id = (uint?)(long?)table[$"{nameof(area_id)}"] ?? 0;
            pos = new Vector3(GetNum(table[$"{nameof(pos)}.x"]) ?? 0, GetNum(table[$"{nameof(pos)}.y"]) ?? 0, GetNum(table[$"{nameof(pos)}.z"]) ?? 0);
            rot = new Vector3(GetNum(table[$"{nameof(rot)}.x"]) ?? 0, GetNum(table[$"{nameof(rot)}.y"]) ?? 0, GetNum(table[$"{nameof(rot)}.z"]) ?? 0);
            vision_level = (VisionLevelType?)table[$"{nameof(vision_level)}"] ?? VisionLevelType.VISION_LEVEL_NORMAL;

            SceneId = group.SceneId;
            BlockId = group.BlockId;
            GroupId = group.GroupId;

            static float? GetNum(object num)
            {
                if (num.GetType() == typeof(double?) || num.GetType() == typeof(double))
                    return (float?)(double?)num;
                else
                    return (uint?)(long?)num;
            }
        }
    }
    public class Monster : SpawnInfo
    {
        public readonly uint monster_id;
        public readonly uint level;
        public readonly string? drop_tag;
        public readonly uint pose_id;

        public Monster(LuaTable table, SceneGroup group) : base(table, group)
        {
            monster_id = (uint)(long)table[$"{nameof(monster_id)}"];
            level = (uint?)(long?)table[$"{nameof(level)}"] ?? 0;
            drop_tag = (string?)table[$"{nameof(drop_tag)}"] ?? "";
            pose_id = (uint?)(long?)table[$"{nameof(pose_id)}"] ?? 0;
        }
    }
    public class Npc : SpawnInfo
    {
        public readonly uint npc_id;
        public readonly uint room;

        public Npc(LuaTable table, SceneGroup group) : base(table, group)
        {
            npc_id = (uint?)(long?)table[$"{nameof(npc_id)}"] ?? 0;
            room = (uint?)(long?)table[$"{nameof(room)}"] ?? 0;
        }
    }
    public class Gadget : SpawnInfo
    {
        public readonly uint gadget_id;
        public readonly uint level;
        public readonly string? drop_tag;
        public readonly uint route_id;
        public readonly bool showcutscene;
        public readonly bool isOneoff;
        public readonly bool persistent;
        public readonly Explore? explore;
        public readonly BossChest? boss_chest;
        public readonly uint chest_drop_id;
        public readonly uint drop_count;
        public readonly bool is_blossom_chest;

        public Gadget(LuaTable table, SceneGroup group) : base(table, group)
        {
            if (table[$"{nameof(explore)}"] != null)
                explore = new Explore(table[$"{nameof(explore)}"] as LuaTable);
            if (table[$"{nameof(boss_chest)}"] != null)
                boss_chest = new BossChest(table[$"{nameof(boss_chest)}"] as LuaTable);
            gadget_id = (uint?)(long?)table[$"{nameof(gadget_id)}"] ?? 0;
            level = (uint?)(long?)table[$"{nameof(level)}"] ?? 0;
            drop_tag = (string?)table[$"{nameof(drop_tag)}"] ?? "";
            route_id = (uint?)(long?)table[$"{nameof(route_id)}"] ?? 0;
            showcutscene = (bool?)table[$"{nameof(showcutscene)}"] ?? false;
            isOneoff = (bool?)table[$"{nameof(isOneoff)}"] ?? false;
            persistent = (bool?)table[$"{nameof(persistent)}"] ?? false;
            is_blossom_chest = (bool?)table[$"{nameof(is_blossom_chest)}"] ?? false;
            chest_drop_id = (uint?)(long?)table[$"{nameof(chest_drop_id)}"] ?? 0;
            drop_count = (uint?)(long?)table[$"{nameof(drop_count)}"] ?? 0;
        }

        public class Explore
        {
            public readonly string name;
            public readonly uint exp;

            public Explore(LuaTable table)
            {
                name = (string?)table[$"{nameof(name)}"] ?? "";
                exp = (uint?)(long?)table[$"{nameof(exp)}"] ?? 0;
            }
        }

        public class BossChest
        {
            public readonly uint monster_config_id;
            public readonly uint resin;
            public readonly uint life_time;
            public readonly uint take_num;

            public BossChest(LuaTable table)
            {
                monster_config_id = (uint?)(long?)table[$"{nameof(monster_config_id)}"] ?? 0;
                resin = (uint?)(long?)table[$"{nameof(resin)}"] ?? 0;
                life_time = (uint?)(long?)table[$"{nameof(life_time)}"] ?? 0;
                take_num = (uint?)(long?)table[$"{nameof(take_num)}"] ?? 0;
            }
        }
    }

    public class Variable
    {
        public readonly uint configId;
        public readonly string name;
        public readonly int value;
        public readonly bool no_refresh;
        public Variable(LuaTable table)
        {
            configId = (uint?)(long?)table[$"{nameof(configId)}"] ?? 0;
            name = (string?)table[$"{nameof(name)}"] ?? "";
            value = (int?)(long?)table[$"{nameof(value)}"] ?? 0;
            no_refresh = (bool?)table[$"{nameof(no_refresh)}"] ?? false;
        }
    }

    public class Region
    {
        public readonly uint config_id;
        public readonly RegionShape shape;
        public readonly float radius;
        public readonly Vector3 pos;
        public readonly uint area_id;

        public Region(LuaTable table)
        {
            config_id = (uint?)(long?)table[$"{nameof(config_id)}"] ?? 0;
            shape = (RegionShape?)table[$"{nameof(shape)}"] ?? RegionShape.REGION_NONE;
            radius = table[$"{nameof(radius)}"] is double ? (float?)(double?)table[$"{nameof(radius)}"] ?? 0 : BitConverter.UInt32BitsToSingle((uint?)(long?)table[$"{nameof(radius)}"] ?? 0);
            pos = new Vector3((float?)(double?)table[$"{nameof(pos)}.x"] ?? 0, (float?)(double?)table[$"{nameof(pos)}.y"] ?? 0, (float?)(double?)table[$"{nameof(pos)}.z"] ?? 0);
            area_id = (uint?)(long?)table[$"{nameof(area_id)}"] ?? 0;
        }
    }

    public class Trigger
    {
        public readonly uint config_id;
        public readonly string name;
        public readonly EventType @event;
        public readonly string source;
        public readonly string condition;
        public readonly string action;
        public readonly uint trigger_count;
        public readonly bool forbid_guest;

        public Trigger(LuaTable table)
        {
            config_id = (uint?)(long?)table[$"{nameof(config_id)}"] ?? 0;
            name = (string?)table[$"{nameof(name)}"] ?? "";
            @event = (EventType?)table[$"event"] ?? EventType.EVENT_NONE;
            source = (string?)table[$"{nameof(source)}"] ?? "";
            condition = (string?)table[$"{nameof(condition)}"] ?? "";
            action = (string?)table[$"{nameof(action)}"] ?? "";
            trigger_count = (uint?)(long?)table[$"{nameof(trigger_count)}"] ?? 0;
            forbid_guest = (bool?)table[$"{nameof(forbid_guest)}"] ?? false;
        }
    }

    // for reference
    private struct SeelieVariable1
    {
        public uint point_sum;
        public uint route_2;
        public uint gadget_seelie;
        public uint final_point;
    }
}
