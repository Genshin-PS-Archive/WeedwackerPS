using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class SignInCondExcelConfig
{
	public uint configId;
	public LogicType condComb;
	public SignInCondConfig[] condList;
	public uint totalDayCount;
}