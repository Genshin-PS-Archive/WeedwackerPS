using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Systems.Script.Scene;

internal class SceneScriptManager
{
    private readonly uint SceneId;

    public SceneInfo Info;
    public World.Scene Scene;
    public Dictionary<uint, Dictionary<string, int>> GroupVariables { get; private set; } = new(); // <groupId, <key, value>>
    public ScriptMonsterSpawnService ScriptMonsterSpawnService;

    public SceneScriptManager(uint sceneId)
    {
        SceneId = sceneId;
    }

    public static Task<SceneScriptManager> CreateAsync(uint sceneId, string scriptPath)
    {
        SceneScriptManager? scene = new(sceneId);
        return scene.InitializeAsync();
    }

    private async Task<SceneScriptManager> InitializeAsync()
    {
        return this;
    }
    internal Task CallEvent(EventType eventType, ScriptArgs scriptArgs)
    {
        throw new NotImplementedException();
    }

    internal int GetGroupVariable(uint block, uint groupId, string key, int defaultVal = default)
    {
        if (GroupVariables.TryGetValue(groupId, out Dictionary<string, int> variables))
        {
            if (variables.TryGetValue(key, out int value))
                return value;
            else
            {
                variables.Add(key, defaultVal);
                return defaultVal;
            }
        }
        else if (defaultVal != default)
        {
            GroupVariables.Add(groupId, new Dictionary<string, int> { { key, defaultVal } });
            return defaultVal;
        }
        else
        {
            int value = Info.BlocksInfo[block].GetSceneGroup(groupId).variables.Find(w => w.name == key).value;
            GroupVariables.Add(groupId, new Dictionary<string, int> { { key, value } });
            return value;
        }
    }

    internal void SetGroupVariable(uint group, string key, int value)
    {
        if (GroupVariables.TryGetValue(group, out Dictionary<string, int> variables))
            if (variables.ContainsKey(key))
                variables[key] = value;
            else
                variables.Add(key, value);
        else
            GroupVariables.Add(group, new Dictionary<string, int>() { { key, value } });
    }

    internal int RefreshGroup(int groupId, int suite)
    {
        throw new NotImplementedException();
    }

    internal int RemoveGroupSuite(uint blockId, int groupId, int suite)
    {
        throw new NotImplementedException();
    }
}
