namespace Weedwacker.GameServer.Data.Excel;

[Columns(21)]
public class HomeWorldPlantExcelConfig
{
	[ColumnIndex(0)]
	public uint seedID;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] gatherIdVec;
	[ColumnIndex(2)]
	public uint? homeGatherID;
	[ColumnIndex(3)]
	public uint? bundleID;
	[ColumnIndex(4)]
	public uint? dropID;
	[ColumnIndex(5)][TsvArray(3)]
	public ConfigHomeGather[] configHomeGather;
	public uint fieldID;
	[ColumnIndex(17)]
	public uint time;
	[ColumnIndex(18)]
	public uint sproutTime;
	[ColumnIndex(19)]
	public uint collectNum;
	[ColumnIndex(20)]
	public uint sproutGadgetID;
	public uint order;
	public string inteeIconName;
	public uint inteeNameTextMapHash;

	[Columns(4)]
	public class ConfigHomeGather
	{
		[ColumnIndex(0)]
		public uint? homeGatherID;
		[ColumnIndex(1)]
		public uint? bundleID;
		[ColumnIndex(2)]
		public uint? dropID;
		[ColumnIndex(3)]
		public uint? weight;
	}
}