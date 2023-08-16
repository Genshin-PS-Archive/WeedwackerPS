using Weedwacker.GameServer.Data.BinOut.Shared;

namespace Weedwacker.GameServer.Data.BinOut.Ability.Temp.AbilityMixins;

public class ShootFromCameraMixin : ConfigAbilityMixin
{
	public string ui_message_id;
	public Vector offsetFromCamera;
	public string gvBulletInitPos;
	public string gvBulletForward;
	public ConfigAbilityAction[] actionList;
}
