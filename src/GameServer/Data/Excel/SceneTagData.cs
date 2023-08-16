using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(11)]
public class SceneTagData
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public string sceneTagName;
	[ColumnIndex(2)]
	public uint sceneId;
	[ColumnIndex(3)]
	public bool isDefaultValid;
	[ColumnIndex(4)]
	public bool isSkipLoading;
	public bool isIndependent;
	[ColumnIndex(5)][TsvArray(2)]
	public SceneTagCond[] cond;


	[Columns(3)]
	public class SceneTagCond
	{
		[ColumnIndex(0)]
		public SceneTagCondType? condType;
		[ColumnIndex(1)]
		public long? param1;
		[ColumnIndex(2)]
		public long? param2;
	}
}
