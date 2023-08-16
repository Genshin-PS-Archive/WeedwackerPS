namespace Weedwacker.GameServer.Data.Excel;

[Columns(17)]
public class ActivityPhotographExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint activityId;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] photoPosIdList;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] photoUseableWidgetList;

	//not in client
	[ColumnIndex(4)]
	public uint maxFov;
	[ColumnIndex(5)]
	public uint minFov;
	[ColumnIndex(6)]
	public uint defaultFov;
	[ColumnIndex(7)]
	public uint panUp;
	[ColumnIndex(8)]
	public uint panDown;
	[ColumnIndex(9)]
	public uint panLeft;
	[ColumnIndex(10)]
	public uint panRight;
	[ColumnIndex(11)]
	public uint QuestAcceptMarkID;
	[ColumnIndex(12)]
	public uint PushTipsID;
	[ColumnIndex(13)]
	public uint billBoardRadius;
	[ColumnIndex(14)]
	public uint PhotoWQ;
	[ColumnIndex(15)][TsvArray(',')]
	public uint[] SkyPalaceLQ; //????
	[ColumnIndex(16)][TsvArray(',')]
	public uint[] DadaliaLQ; //????
}