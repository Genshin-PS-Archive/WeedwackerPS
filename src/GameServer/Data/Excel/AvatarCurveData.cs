using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(13)]
public class AvatarCurveData
{
	[ColumnIndex(0)]
	public uint level;
	[ColumnIndex(1)][TsvArray(4)]
	public GrowCurveInfo[] curveInfos;

	public Tuple<ArithType, float> GetArith(GrowCurveType type)
	{
		foreach (GrowCurveInfo curveInfo in curveInfos)
		{
			if (curveInfo.type == type) return Tuple.Create(curveInfo.arith, curveInfo.value);
		}
		Logger.WriteErrorLine("Could not find value for " + type + " for avatar level:" + level);
		return Tuple.Create(ArithType.ARITH_MULTI, (float)1.0);
	}
}