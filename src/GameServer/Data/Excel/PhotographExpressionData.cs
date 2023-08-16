using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class PhotographExpressionData : FetterConfig
{
	public string emotionName;
	public string phonemeName;
	public string icon;
	public uint emotionDescriptionTextMapHash;
	public uint unlockDescTextMapHash;

	public static async Task Load<Key>(string path, Func<PhotographExpressionData, Key> keySelector, IDictionary<Key, PhotographExpressionData> map)
	{
		using StringReader? sr = new(await File.ReadAllTextAsync(path));
		//skip header
		sr.ReadLine();
		string? line;
		while ((line = sr.ReadLine()) != null)
		{
			if (string.IsNullOrEmpty(line)) continue;
			PhotographExpressionData obj = ParseLine(line);
			map.Add(keySelector(obj), obj);
		}
	}

	private static PhotographExpressionData ParseLine(string line)
	{
		PhotographExpressionData obj = new();
		ReadOnlySpan<char> span = line.AsSpan();
		int t01 = span.IndexOf('\t');
		obj.fetter_id = uint.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.avatarId = uint.Parse(span[..t01]);
		obj.openConds = new FetterConditionConfig[] { new() };
		obj.openConds[0].paramList = new uint[1];
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.openConds[0].condType = (FetterCondType)ulong.Parse(span[..t01]);
		if (!span[(t01 + 1)..].IsEmpty)
			obj.openConds[0].paramList[0] = uint.Parse(span[(t01 + 1)..]);
		return obj;
	}
}
