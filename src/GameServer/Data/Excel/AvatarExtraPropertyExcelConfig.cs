namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(8)]
public class AvatarExtraPropertyExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	[ColumnIndex(1)][TsvArray(3)]
	public AddAttribute[] AddAttributes;
	[ColumnIndex(7)]
	public uint MaxQuantity;

	[Columns(2)]
	public class AddAttribute
	{
		[ColumnIndex(0)]
		public uint? type;
		[ColumnIndex(1)]
		public uint? value;
	}
}
