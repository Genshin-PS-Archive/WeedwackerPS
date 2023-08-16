using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(1)]
public class DocumentExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public uint titleTextMapHash;
	public uint contentLocalizedId;
	public string previewPath;
	public DocumentType documentType;
	public string videoPath;
	public uint subtitleID;
}