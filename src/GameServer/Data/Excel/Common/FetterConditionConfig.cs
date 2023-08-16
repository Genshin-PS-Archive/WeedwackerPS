using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(2)]
public class FetterConditionConfig
{
	public FetterCondType condType;
	public uint[] paramList;
}