using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class HomeWorldFurnitureTypeExcelConfig
{
	[ColumnIndex(0)]
	public uint typeID;
	[ColumnIndex(1)]
	public uint typeCategoryID;
	public uint typeNameTextMapHash;
	public uint typeName2TextMapHash;
	public string tabIcon;
	[ColumnIndex(2)]
	public FurnitureDeployType sceneType;
	public uint cameraID;
	public uint bagPageOnly;
	[ColumnIndex(3)]
	public bool isShowInBag;
	[ColumnIndex(4)]
	public uint? limit;
}