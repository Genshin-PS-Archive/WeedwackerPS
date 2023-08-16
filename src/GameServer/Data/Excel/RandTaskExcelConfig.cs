using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class RandTaskExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	public uint titleTextMapHash;
    [ColumnIndex(1)]
    public RandTaskContentType contentType;
    [ColumnIndex(2)]
    public uint reward;
	public bool needUI;
	public uint targetTextMapHash;
	public uint enterDistance;
	public uint exitDistance;
}