using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class ProductPsnCompensationDetailConfig : ProductDetailConfig
{
	public PackageContentConfig[] contentVec;
	public uint mailConfigId;
	public uint limitCount;
}