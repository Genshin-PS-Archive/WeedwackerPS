namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class FleurFairBuffEnergyStatExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint galleryId;
	[ColumnIndex(2)]
	public string statParam;
	[ColumnIndex(3)]
	public uint priority;
	public uint titleTextMapHash;
}