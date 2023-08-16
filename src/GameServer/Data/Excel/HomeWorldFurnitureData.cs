using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(19)]
public class HomeWorldFurnitureData : ItemConfig
{
	[ColumnIndex(0)][TsvArray(3)]
	public uint?[] furnitureGadgetID;
	[ColumnIndex(3)][TsvArray(2)]
	public uint?[] furnType;
	[ColumnIndex(5)]
	public FurnitureDeploySurfaceType surfaceType;
	[ColumnIndex(6)]
	public uint? arrangeLimit;	
	public uint isSpecialFurniture;
	[ColumnIndex(7)]
	public SpeicalFurnitureType? specialFurnitureType;
	[ColumnIndex(8)]
	public uint? roomSceneID;
	public uint gridStyle;
	[ColumnIndex(9)]
	public uint comfort;
	[ColumnIndex(10)]
	public uint stackLimit;
	[ColumnIndex(11)]
	public uint cost;
	[ColumnIndex(12)]
	public uint discountCost;
	[ColumnIndex(13)]
	public uint? isCombinableLight;
	public float height;
	public uint canFloat;
	public uint isUnique;
	public string itemIcon;
	public string effectIcon;
	public float clampDistance;
	public float editorClampDistance;
	public uint deployGlitchIndex;
	[ColumnIndex(14)]
	public uint rankLevel;
	[ColumnIndex(15)]
	public string jsonName;
	[ColumnIndex(17)]
	public uint? pushTipsId;
	[ColumnIndex(18)]
	public GroupRecordType? groupRecordType;

	//not in client
	[ColumnIndex(16)]
	public string graphicTutorial;
}
