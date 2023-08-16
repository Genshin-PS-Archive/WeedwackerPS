using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(55)]
public class WeaponCurveData
{
	[ColumnIndex(0)]
	public uint level;
	[ColumnIndex(1)][TsvArray(18)]
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

	public static float CalcValue(float value, Tuple<ArithType, float> curve)
	{
		switch (curve.Item1)
		{
			case ArithType.ARITH_MULTI:
				return value * curve.Item2;
			case ArithType.ARITH_ADD:
				return value + curve.Item2;
			default:
				Logger.WriteErrorLine("Unknown Weapon curve operation");
				goto case ArithType.ARITH_MULTI;
		}
	}
}
