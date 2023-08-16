using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(9)]
public abstract class ItemConfig
{
	[ColumnIndex(0)]
	public uint id;
	public uint nameTextMapHash;
	public uint descTextMapHash;
	public string icon;
	[ColumnIndex(1)]
	public ItemType itemType;
	[ColumnIndex(2)]
	public uint? weight;
	[ColumnIndex(3)]
	public uint? rank;
	[ColumnIndex(4)]
	public uint? gadgetId;
	[ColumnIndex(5)]
	public bool? dropable;
	[ColumnIndex(6)]
	public uint? useLevel;
	[ColumnIndex(7)]
	public uint? globalItemLimit;

	//not in client
	[ColumnIndex(8)]
	public string NumericType;
}
