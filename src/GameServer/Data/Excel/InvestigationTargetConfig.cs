using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class InvestigationTargetConfig : WatcherConfig
{
	public uint questId;
	public uint investigationId;
	public uint rewardId;
	public string icon;
	public string image;
	public uint infoDesTextMapHash;
	public QuestGuide guide;

	public static async Task Load<Key>(string path, Func<InvestigationTargetConfig, Key> keySelector, IDictionary<Key, InvestigationTargetConfig> map)
	{
		using StringReader? sr = new(await File.ReadAllTextAsync(path));
		//skip header
		sr.ReadLine();
		string? line;
		while ((line = sr.ReadLine()) != null)
		{
			if (string.IsNullOrEmpty(line)) continue;
			InvestigationTargetConfig obj = ParseLine(line);
			map.Add(keySelector(obj), obj);
		}
	}

	private static InvestigationTargetConfig ParseLine(string line)
	{
		InvestigationTargetConfig obj = new();
		ReadOnlySpan<char> span = line.AsSpan();
		int t01 = span.IndexOf('\t');
		obj.id = uint.Parse(span[..t01]);
		obj.triggerConfig = new();
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.triggerConfig.triggerType = (WatcherTriggerType)uint.Parse(span[..t01]);
		obj.triggerConfig.paramList = new string[3];
		for (int i = 0; i < 3; i++)
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
		obj.investigationId = uint.Parse(span[..t01]);
		obj.rewardId = uint.Parse(span[(t01 + 1)..]);
		return obj;
	}
}