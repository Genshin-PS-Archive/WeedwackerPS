using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(30)]
public class GalleryExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public GalleryType type;
	[ColumnIndex(2)][TsvArray(4)]
	public string[] param;
	[ColumnIndex(6)]
	public bool isEnableSinglePrepare;
	[ColumnIndex(7)]
	public uint? singlePrepareTime;
	[ColumnIndex(8)]
	public bool isEnableMpPrepare;
	[ColumnIndex(9)]
	public uint? mpPrepareTime;
	[ColumnIndex(10)]
	public bool isPermitInputInPrepare;
	[ColumnIndex(11)]
	public bool canInterruptByClient;
	[ColumnIndex(12)]
	public uint? sceneId;
	[ColumnIndex(13)]
	public uint? controlGroupId;
	[ColumnIndex(14)][TsvArray(',')]
	public uint[] groupId;
	[ColumnIndex(15)]
	public uint? groupFurnitureId;
	[ColumnIndex(16)]
	public uint? revivePointGroupId;
	[ColumnIndex(17)]
	public uint? revivePointConfigId;
	[ColumnIndex(18)]
	public string abilityGroup;
	[ColumnIndex(19)][TsvArray(',')]
	public string[] abilityGroupList;
	[ColumnIndex(20)]
	public string limitRegion;
	public uint centerSceneId;
	public float[] centerPosList;
	public uint centerRadius;
	public uint duration;
	public uint nameTextMapHash;
	public uint descTextMapHash;
	public uint galleryNameTextMapHash;
	public uint galleryMSGTextMapHash;
	public string pic;
	public uint targetTextMapHash;
	public string startAudioValues;
	public string endAudioValues;
	[ColumnIndex(25)][TsvArray(',')]
	public string[] selectable_ability_groups;
	[ColumnIndex(26)][TsvArray(',')]
	public string[] teamAbilityGroupList;
	[ColumnIndex(27)][TsvArray(',')]
	public string[] selectableTeamAbilityGroupList;
	[ColumnIndex(28)]
	public bool isDisableGroupDefaultReviseLevel;

	//not in client
	[ColumnIndex(21)]
	public uint? mpTransferPointGroupId;
	[ColumnIndex(22)]
	[TsvArray(',')]
	public uint[] mpTransferPointConfigId;
	[ColumnIndex(23)]
	public uint? mpTeleportTimeout;
	[ColumnIndex(24)]
	[TsvArray(',')]
	public uint[] mpPreloadGroupId;
	[ColumnIndex(29)]
	[TsvArray(',')]
	public uint[] GalleryCompletionMethod;
}