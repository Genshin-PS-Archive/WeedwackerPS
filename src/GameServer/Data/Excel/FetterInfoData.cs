using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class FetterInfoData : FetterConfig
{
	public bool isHiden;
	public uint infoBirthMonth;
	public uint infoBirthDay;
	public uint avatarNativeTextMapHash;
	public uint avatarVisionBeforTextMapHash;
	public uint avatarConstellationBeforTextMapHash;
	public uint avatarTitleTextMapHash;
	public uint avatarDetailTextMapHash;
	public AssocType avatarAssocType;
	public uint cvChineseTextMapHash;
	public uint cvJapaneseTextMapHash;
	public uint cvEnglishTextMapHash;
	public uint cvKoreanTextMapHash;
	public uint avatarVisionAfterTextMapHash;
	public uint avatarConstellationAfterTextMapHash;

	public static async Task Load<Key>(string path, Func<FetterInfoData, Key> keySelector, IDictionary<Key, FetterInfoData> map)
	{
		using StringReader? sr = new(await File.ReadAllTextAsync(path));
		//skip header
		sr.ReadLine();
		string? line;
		while ((line = sr.ReadLine()) != null)
		{
			if (string.IsNullOrEmpty(line)) continue;
			FetterInfoData obj = ParseLine(line);
			map.Add(keySelector(obj), obj);
		}
	}

	private static FetterInfoData ParseLine(string line)
	{
		FetterInfoData obj = new();
		ReadOnlySpan<char> span = line.AsSpan();
		int t01 = span.IndexOf('\t');
		obj.fetter_id = uint.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.avatarId = uint.Parse(span[..t01]);
		//finishConds
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.finishConds = new FetterConditionConfig[] { new() };
		obj.finishConds[0].condType = (FetterCondType)ulong.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		//array
		ReadOnlySpan<char> arr = span[..t01];
		List<uint> paramList = new();
		while(true)
		{
			int tarr = arr.IndexOf(';');
			if (tarr == -1)
			{
				if(!arr.IsEmpty)
					paramList.Add(uint.Parse(arr));
				break;
			}
			else
			{
				paramList.Add(uint.Parse(arr[..tarr]));
				arr = arr[(tarr + 1)..];
			}
		}
		obj.finishConds[0].paramList = paramList.ToArray();
		//array
		//finishConds
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.infoBirthMonth = uint.Parse(span[..t01]);
		obj.infoBirthDay = uint.Parse(span[(t01 + 1)..]);
		return obj;
	}
}
