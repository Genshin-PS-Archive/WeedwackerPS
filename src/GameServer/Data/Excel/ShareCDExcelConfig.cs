namespace Weedwacker.GameServer.Data.Excel;

public class ShareCDExcelConfig
{
	public uint id;
	public CoolDownInfo[] coolDownList;
	public bool isUseRealTime;
	public bool isUseExternalSystem;

	public class CoolDownInfo
	{
		public float coolDownTime;
		public uint token;
	}
}