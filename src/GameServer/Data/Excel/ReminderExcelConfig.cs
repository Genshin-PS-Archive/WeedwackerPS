using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class ReminderExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public uint speakerTextMapHash;
	public uint contentTextMapHash;
	public float delay;
	[ColumnIndex(1)]
	public float showTime;
	public uint nextReminderId;
	public string soundEffect;
	public ReminderStyleType style;
	public bool hasAudio;
}