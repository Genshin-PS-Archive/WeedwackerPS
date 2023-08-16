using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(4)]
public class BaseServerBuffConfig
{
	[ColumnIndex(0)]
	public uint serverBuffId;
	[ColumnIndex(1)]
	public ServerBuffType serverBuffType;
	[ColumnIndex(2)]
	public string abilityName;
	[ColumnIndex(3)]
	public string modifierName;
}