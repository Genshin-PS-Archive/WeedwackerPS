using System.Numerics;
using NLua;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Systems.Script.Scene;

internal class DummyPoints
{
    private Lua LuaState;
    private uint SceneId;

    public Dictionary<string, Position> dummy_points;

    private DummyPoints(Lua lua, uint sceneId)
    {
        LuaState = lua;
        SceneId = sceneId;
    }

    public static DummyPoints? Create(Lua lua, uint sceneId, string scriptPath)
    {
        DummyPoints points = new(lua, sceneId);
        return points.Initialize(scriptPath);
    }

    private DummyPoints? Initialize(string scriptPath)
    {
        if (!SetupLuaScene(scriptPath))
            return null;
        
        SetupLuaData();

        return this;
    }

    private bool SetupLuaScene(string scriptPath)
    {
        FileInfo dummyInfo = new(Path.Combine(scriptPath, "scene", $"{SceneId}", $"scene{SceneId}_{nameof(dummy_points)}.lua"));
        string dummyScript = dummyInfo.FullName.Replace(@"\", @"\\");

        if (!File.Exists(dummyScript))
        {
            Logger.DebugWriteLine($"Could not find file {dummyInfo.FullName}");
            return false;
        }

        try
        {
            LuaState.DoString($$"""
                _SCENE{{SceneId}}_POINTS = {}
                loadfile("{{dummyScript}}", "bt", _SCENE{{SceneId}}_POINTS)()
                """);
        }
        catch
        {
            Logger.DebugWriteLine($"Failed to load {dummyInfo.Name}");
            return false;
        }

        return true;
    }

    private void SetupLuaData()
    {
        LuaTable? dummyPointTable = (LuaTable?)LuaState[$"_SCENE{SceneId}_POINTS.{nameof(dummy_points)}"];
        if (dummyPointTable != null)
            dummy_points = LuaState.GetTableDict(dummyPointTable).ToDictionary(w => (string)w.Key, w => new Position(w.Value as LuaTable));
    }

    public class Position
    {
        private LuaTable Table;
        public Vector3 pos => new((float?)(double?)Table[$"{nameof(pos)}.x"] ?? 0, (float?)(double?)Table[$"{nameof(pos)}.y"] ?? 0, (float?)(double?)Table[$"{nameof(pos)}.z"] ?? 0);
        public Vector3 rot => new((float?)(double?)Table[$"{nameof(rot)}.x"] ?? 0, (float?)(double?)Table[$"{nameof(rot)}.y"] ?? 0, (float?)(double?)Table[$"{nameof(rot)}.z"] ?? 0);

        public Position(LuaTable luaTable)
        {
            Table = luaTable;
        }
    }
}
