namespace Weedwacker.GameServer.Data.Excel;

[Columns(13)]
public class RandTaskRewardConfig
{
	[ColumnIndex(0)]
	public uint ID;
    [ColumnIndex(1)][TsvArray(12)]
    public RandTaskDropConfig[] dropVec;

    [Columns(1)]
    public class RandTaskDropConfig
	{
		[ColumnIndex(0)]
		public uint dropId;
	}
}