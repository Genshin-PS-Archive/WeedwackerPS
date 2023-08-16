using Google.Protobuf;
using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.CombatInvocationsNotify)]
	private async Task HandleCombatInvocationsNotify(byte[] header, byte[] payload)
	{
		CombatInvocationsNotify p = CombatInvocationsNotify.Parser.ParseFrom(payload);
		foreach (CombatInvokeEntry entry in p.InvokeList)
		{
			IBufferMessage info;
			ByteString data = entry.CombatData;
			switch (entry.ArgumentType)
			{
				case CombatTypeArgument.None:
					goto default;
					break;
				case CombatTypeArgument.EvtBeingHit:
					info = EvtBeingHitInfo.Parser.ParseFrom(data);
					EvtBeingHitInfo? hitInfo = info as EvtBeingHitInfo;

					if (hitInfo != null)
					{
						await HandleEvtBeingHit(hitInfo);
					}

					break;

				case CombatTypeArgument.AnimatorStateChanged:
					info = EvtAnimatorStateChangedInfo.Parser.ParseFrom(data);
					//TODO
					break;
				case CombatTypeArgument.FaceToDir:
					info = EvtFaceToDirInfo.Parser.ParseFrom(data);
					//TODO
					break;
				case CombatTypeArgument.SetAttackTarget:
					info = EvtSetAttackTargetInfo.Parser.ParseFrom(data);
					//TODO
					break;
				case CombatTypeArgument.RushMove:
					info = EvtRushMoveInfo.Parser.ParseFrom(data);
					//TODO
					break;
				case CombatTypeArgument.AnimatorParameterChanged:
					info = EvtAnimatorParameterInfo.Parser.ParseFrom(data);
					//TODO
					break;
				case CombatTypeArgument.EntityMove: // Seems to only handle avatar entities, despite the generic name...
					info = EntityMoveInfo.Parser.ParseFrom(data);
					EntityMoveInfo? moveInfo = info as EntityMoveInfo;
					if (Player.TeamManager.CurrentAvatarEntity?.EntityId == moveInfo.EntityId)
					{
						AvatarEntity? avatar = Player.TeamManager.CurrentAvatarEntity;
						await avatar.MoveAsync(moveInfo);
					}
					break;
				case CombatTypeArgument.SyncEntityPosition:
					info = EvtSyncEntityPositionInfo.Parser.ParseFrom(data);
					//TODO
					break;
				case CombatTypeArgument.SteerMotionInfo:
					info = EvtCombatSteerMotionInfo.Parser.ParseFrom(data);
					//TODO
					break;
				case CombatTypeArgument.ForceSetPosInfo:
					info = EvtCombatForceSetPosInfo.Parser.ParseFrom(data);
					//TODO
					break;
				case CombatTypeArgument.CompensatePosDiff:
					info = EvtCompensatePosDiffInfo.Parser.ParseFrom(data);
					//TODO
					break;
				case CombatTypeArgument.MonsterDoBlink:
					info = EvtMonsterDoBlink.Parser.ParseFrom(data);
					//TODO
					break;
				case CombatTypeArgument.FixedRushMove:
					info = EvtFixedRushMove.Parser.ParseFrom(data);
					//TODO
					break;
				case CombatTypeArgument.SyncTransform:
					info = EvtSyncTransform.Parser.ParseFrom(data);
					//TODO
					break;
				case CombatTypeArgument.LightCoreMove:
					info = EvtLightCoreMove.Parser.ParseFrom(data);
					//TODO
					break;
				case CombatTypeArgument.BeingHealedNtf:
					//TODO
					info = EvtBeingHealedNotify.Parser.ParseFrom(data);
					break;
				case CombatTypeArgument.GrapplingHookMove:
					//TODO
					info = EvtGrapplingHookMove.Parser.ParseFrom(data);
					break;
				case CombatTypeArgument.SkillAnchorPositionNtf:
					//TODO
					info = EvtSyncSkillAnchorPosition.Parser.ParseFrom(data);
					break;
				default:
					info = new EntityMoveInfo();
					break;
			}

			Player.CombatInvNotifyList.AddEntry(entry, entry.ForwardType);
#if DEBUG
			if (GameServer.Configuration.Server.LogOptions.LogCombatInvocations)
			{
				if (!GameServer.Configuration.Server.LogOptions.CombatArgTypeBlacklist.Contains(entry.ArgumentType))
				{
					Connection.LogCombatInvocation("RECV combat invoke: ", entry, info.GetType());
				}
			}
#endif
		}
	}

	private async Task HandleEvtBeingHit(EvtBeingHitInfo hitInfo)
	{
		await Player.Scene.HandleAttackAsync(hitInfo.AttackResult);
	}
}
