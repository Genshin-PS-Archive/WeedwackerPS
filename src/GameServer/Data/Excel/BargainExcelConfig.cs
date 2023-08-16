namespace Weedwacker.GameServer.Data.Excel;

[Columns(17)]
public class BargainExcelConfig
{
	public uint quest_id;
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)][TsvArray(';')]
	public uint[] dialog_id;
	[ColumnIndex(2)][TsvArray(';')]
	public uint[] expected_value;
	[ColumnIndex(3)]
	public uint space;
	[ColumnIndex(4)][TsvArray(3)]
	public uint[] success_talk_id;
	[ColumnIndex(7)]
	public uint fail_talk_id;
	public uint mood_npc_id;
	[ColumnIndex(8)]
	public uint mood_upper_limit;
	[ColumnIndex(9)][TsvArray(';')]
	public uint[] random_mood;
	[ColumnIndex(10)]
	public uint mood_alert_limit;
	[ColumnIndex(11)]
	public int mood_low_limit;
	public uint mood_low_limit_textTextMapHash;
	[ColumnIndex(12)]
	public uint single_fail_mood_deduction;
	[ColumnIndex(13)][TsvArray(2)]
	public uint[] single_fail_talk_id;
	[ColumnIndex(15)]
	public bool delete_item;
	[ColumnIndex(16)]
	public uint item_id;
	public uint title_textTextMapHash;
	public uint afford_textTextMapHash;
	public uint storage_textTextMapHash;
	public uint mood_hint_textTextMapHash;
	public uint mood_desc_textTextMapHash;
}