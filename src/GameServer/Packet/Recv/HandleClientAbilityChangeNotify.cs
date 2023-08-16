using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Enums;
using Weedwacker.Shared.Network.Proto;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.ClientAbilityChangeNotify)]
	private async Task HandleClientAbilityChangeNotify(byte[] header, byte[] payload)
    {
        ClientAbilityChangeNotify p = ClientAbilityChangeNotify.Parser.ParseFrom(payload);
        if (Player.Scene.Entities.TryGetValue(p.EntityId, out BaseEntity? entity))
        {
            foreach (AbilityInvokeEntry? invoke in p.Invokes)
            {
                await entity.AbilityManager.HandleAbilityInvokeAsync(invoke, Player);
            }
        }
        else if (Player.Scene.ScriptEntities.TryGetValue(p.EntityId, out IScriptEntity? scriptEntity))
        {
            foreach (AbilityInvokeEntry? invoke in p.Invokes)
            {
                await (scriptEntity as SceneEntity).AbilityManager.HandleAbilityInvokeAsync(invoke, Player);
            }
        }
        else
        {
            entity = Player.TeamManager.ActiveTeam.Values.Where(w => w.EntityId == p.EntityId).FirstOrDefault();
            if (entity != null)
            {
                foreach (AbilityInvokeEntry? invoke in p.Invokes)
                {
                    await entity.AbilityManager.HandleAbilityInvokeAsync(invoke, Player);
                }
            }
            else
#if DEBUG
                if (GameServer.Configuration.Server.LogOptions.LogAbilities is AbilityDebugMode.INFO or AbilityDebugMode.WARN or AbilityDebugMode.ERROR or AbilityDebugMode.ALL)
                    Logger.DebugWriteLine($"Failed to find entity {p.EntityId}");
#endif
        }
    }
}
