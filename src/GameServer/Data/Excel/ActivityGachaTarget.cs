using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

//not in client
[Columns(8)]
public class ActivityGachaTarget
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)]
	public ActivityGachaTargetType Type;
	[ColumnIndex(2)]
	public uint IndexId;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] groupNumbers;
	[ColumnIndex(4)]
	public uint affectProperty;
	[ColumnIndex(5)]
	public uint propertyNumber;
	[ColumnIndex(6)]
	public uint randomWeight;
	[ColumnIndex(7)]
	public uint progressMultiplier;
}
