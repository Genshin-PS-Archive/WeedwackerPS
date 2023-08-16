using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class CustomGadgetRootExcelConfig
{
	[ColumnIndex(0)]
	public uint rootGadgetID;
	public CustomGadgetRootType contextType;
	public string pageTitle;
	[ColumnIndex(1)]
	public string recommendConfig;
	public uint[] tabList;
}