
namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(29)]
public class CreatureExcelConfig : EntityExcelConfig
{
	[ColumnIndex(0)]
	public float hp_base;
	[ColumnIndex(1)]
	public float attack_base;
	[ColumnIndex(2)]
	public float defense_base;
	[ColumnIndex(3)]
	public float critical;
	[ColumnIndex(4)]
	public float antiCritical;
	[ColumnIndex(5)]
	public float criticalHurt;
	[ColumnIndex(6)]
	public float fireSubHurt;
	[ColumnIndex(7)]
	public float grassSubHurt;
	[ColumnIndex(8)]
	public float waterSubHurt;
	[ColumnIndex(9)]
	public float elecSubHurt;
	[ColumnIndex(10)]
	public float windSubHurt;
	[ColumnIndex(11)]
	public float iceSubHurt;
	[ColumnIndex(12)]
	public float rockSubHurt;
	[ColumnIndex(13)]
	public float fireAddHurt;
	[ColumnIndex(14)]
	public float grassAddHurt;
	[ColumnIndex(15)]
	public float waterAddHurt;
	[ColumnIndex(16)]
	public float elecAddHurt;
	[ColumnIndex(17)]
	public float windAddHurt;
	[ColumnIndex(18)]
	public float iceAddHurt;
	[ColumnIndex(19)]
	public float rockAddHurt;
	[ColumnIndex(20)][TsvArray(3)]
	public FightPropGrowConfig[] propGrowCurves;
	[ColumnIndex(26)]
	public float elementMastery;
	[ColumnIndex(27)]
	public float physicalSubHurt;
	[ColumnIndex(28)]
	public float physicalAddHurt;
	public byte prefabPathRagdollHashPre;
	public uint prefabPathRagdollHashSuffix;
}