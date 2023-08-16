namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class AvatarHeroEntityData
{
	[ColumnIndex(0)]
	public uint avatarId;
	[ColumnIndex(1)]
	public string prefabPath;
	//public byte prefabPathHashPre;
	//public uint prefabPathHashSuffix;
	public byte animatorConfigPathHashPre;
	public uint animatorConfigPathHashSuffix;
}
