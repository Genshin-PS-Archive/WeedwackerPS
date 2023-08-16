using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class NpcExcelConfig : EntityExcelConfig
{
	[ColumnIndex(0)]
	public string jsonName;
	[ColumnIndex(1)]
	public string jsonPath;
	//public byte jsonPathHashPre;
	//public uint jsonPathHashSuffix;
	public string alias;
	public string scriptDataPath;
	public string luaDataPath;
	public uint luaDataIndex;
	[ColumnIndex(2)]
	public bool hasCombat;
	[ColumnIndex(3)]
	public bool hasMove;
	[ColumnIndex(4)]
	public bool hasAudio;
	[ColumnIndex(5)]
	public bool isDaily;
	public string dyePart;
	public BillboardType billboardType;
	public string billboardIcon;
	public bool invisiable;
	public bool disableShowName;
	public string templateEmotionPath;
	public byte animatorConfigPathHashPre;
	public uint animatorConfigPathHashSuffix;
	public NPCBodyType bodyType;
	[ColumnIndex(6)]
	public uint? firstMetId; //NpcFirstMet file
	public uint uniqueBodyId;
	public bool isActivityDailyNpc;
	public bool useDynBone;
	public bool skipInitClosetToGround;
	public bool isRelease;
}