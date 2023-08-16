using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class ActivityGachaRobotExcelConfig
{
	[ColumnIndex(0)]
	public uint robotId;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] shapeList;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] colorList;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] actionList;
	[ColumnIndex(4)]
	public ActivityGachaRobot type;
	[ColumnIndex(5)]
	public uint furnitureId;
	public uint materialId;
	public string modelPath;
	public uint animatorID;
	public uint playInterval;
	public uint descTextMapHash;
	public string audio;
}