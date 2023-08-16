using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(21)]
public class FireworksExcelConfig
{
	[ColumnIndex(0)]
	public uint materialID;
	[ColumnIndex(1)][TsvArray(5)]
	public FireworksReformParamConfig[] reformParamList;
	public FireworksType fireworksType;
	public string liftOffEffectName;
	public string explodeEffectName;
	public uint detailedDescTextMapHash;

	[Columns(4)]
	public class FireworksReformParamConfig
	{
		[ColumnIndex(0)]
		public FireworksReformParamType type;
		[ColumnIndex(1)]
		public int standardValue;
		[ColumnIndex(2)]
		public bool isCanReform;
		[ColumnIndex(3)][TsvArray(',')]
		public int[] valueRange;
	}
}