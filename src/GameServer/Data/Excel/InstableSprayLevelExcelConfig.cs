namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(6)]
public class InstableSprayLevelExcelConfig
{
	[ColumnIndex(0)]
	public uint LevelID;
	[ColumnIndex(1)]
	public uint GalleryID;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] BuffPool;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] BuffProbabilityWeights;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] TrialAvatarPool;
	[ColumnIndex(5)][TsvDictionary(':', ',')]
	public Dictionary<uint, uint> MonsterPreview;
}
