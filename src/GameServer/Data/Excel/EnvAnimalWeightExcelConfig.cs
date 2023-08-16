using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(17)]
public class EnvAnimalWeightExcelConfig
{
	[ColumnIndex(0)]
	public EnvironmentType envType;
	[ColumnIndex(1)][TsvArray(4)]
	public EnvironmentWeightType[] weightVec;

	[Columns(4)]
	public class EnvironmentWeightType
	{
		[ColumnIndex(0)]
		public uint? animalId;
		[ColumnIndex(1)]
		public EntityType? entityType;
		[ColumnIndex(2)]
		public uint? weight;
		[ColumnIndex(3)][TsvDictionary(':', ',')]
		public Dictionary<uint, uint>? aliveHourMap;
	}
}