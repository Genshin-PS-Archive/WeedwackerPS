using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class H5ActivityWatcherExcelConfig : WatcherConfig
{
	[ColumnIndex(0)]
	public uint? h5ActivityId;
	public LogicType condComb;
	[ColumnIndex(1)][TsvArray(2)]
	public H5ActivityCondConfig[] condList;
	[ColumnIndex(5)]
	public bool isDailyRefresh;

	public static async Task Load<Key>(string path, Func<H5ActivityWatcherExcelConfig, Key> keySelector, IDictionary<Key, H5ActivityWatcherExcelConfig> map)
	{
		using StringReader? sr = new(await File.ReadAllTextAsync(path));
		//skip header
		sr.ReadLine();
		string? line;
		while ((line = sr.ReadLine()) != null)
		{
			if (string.IsNullOrEmpty(line)) continue;
			H5ActivityWatcherExcelConfig obj = ParseLine(line);
			map.Add(keySelector(obj), obj);
		}
	}

	private static H5ActivityWatcherExcelConfig ParseLine(string line)
	{
		H5ActivityWatcherExcelConfig obj = new();
		ReadOnlySpan<char> span = line.AsSpan();
		int t01 = span.IndexOf('\t');
		obj.id = uint.Parse(span[..t01]);
		obj.triggerConfig = new();
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.triggerConfig.triggerType = (WatcherTriggerType)uint.Parse(span[..t01]);
		obj.triggerConfig.paramList = new string[5];
		for (int i = 0; i < 5; i++)
		{
			span = span[(t01 + 1)..];
			t01 = span.IndexOf('\t');
			obj.triggerConfig.paramList[i] = span[..t01].ToString();
		}
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.progress = uint.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.isDisuse = span[..t01].ToString() == "1";
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.h5ActivityId = uint.Parse(span[..t01]);
		obj.condList = new H5ActivityCondConfig[] { new(), new() };
		for(int i = 0; i < 2; i++)
		{
			span = span[(t01 + 1)..];
			t01 = span.IndexOf('\t');
			obj.condList[i].type = (H5ActivityCondType)uint.Parse(span[..t01]);
			span = span[(t01 + 1)..];
			t01 = span.IndexOf('\t');
			obj.condList[i].paramStr = span[..t01].ToString();
		}
		obj.isDailyRefresh = span[(t01 + 1)..].ToString() == "1";
		return obj;
	}
}