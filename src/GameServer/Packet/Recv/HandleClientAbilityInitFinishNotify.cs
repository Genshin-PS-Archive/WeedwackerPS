using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Enums;
using Weedwacker.Shared.Network.Proto;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.ClientAbilityInitFinishNotify)]
	private async Task HandleClientAbilityInitFinishNotify(byte[] header, byte[] payload)
    {
        ClientAbilityInitFinishNotify proto = ClientAbilityInitFinishNotify.Parser.ParseFrom(payload);
        foreach (AbilityInvokeEntry? invoke in proto.Invokes)
        {
            Player.AbilityInvNotifyList.AddEntry(invoke, invoke.ForwardType, invoke.ForwardPeer);

            if (Player.Scene.ScriptEntities.TryGetValue(invoke.EntityId, out IScriptEntity? scriptEntity) && (scriptEntity as SceneEntity).AbilityManager != null)
                await (scriptEntity as SceneEntity).AbilityManager.HandleAbilityInvokeAsync(invoke, Player);

            else if (Player.Scene.Entities.TryGetValue(invoke.EntityId, out BaseEntity? entity) && entity.AbilityManager != null)
                await entity.AbilityManager.HandleAbilityInvokeAsync(invoke, Player);

            else if (invoke.EntityId == Player.World.LevelEntityId)
                await Player.World.AbilityManager.HandleAbilityInvokeAsync(invoke, Player);

            else
            {
#if DEBUG
                if (GameServer.Configuration.Server.LogOptions.LogAbilities is AbilityDebugMode.INFO or AbilityDebugMode.WARN or AbilityDebugMode.ERROR or AbilityDebugMode.ALL)
                    Logger.DebugWriteLine($"Failed to find entity {invoke.EntityId}");
#endif
                break;
            }
        }
    }
}
