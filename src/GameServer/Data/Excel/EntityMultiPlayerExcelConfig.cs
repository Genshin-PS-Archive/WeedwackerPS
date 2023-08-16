using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class EntityMultiPlayerExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)][TsvArray(2)]
	public EntityPropPerMpConfig[] propPerVec;
	public uint[] endureNumVec;
	public float[] elementShieldPerVec;

	[Columns(2)]
	public class EntityPropPerMpConfig
	{
		[ColumnIndex(0)]
		public FightPropType propType;
		[ColumnIndex(1)][TsvArray(',')]
		public float[] propValueVec;
	}
}