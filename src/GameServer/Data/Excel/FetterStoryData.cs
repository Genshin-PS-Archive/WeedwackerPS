using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class FetterStoryData : FetterConfig
{
	public bool isHiden;
	public uint storyTitleTextMapHash;
	public uint storyContextTextMapHash;
	public uint storyTitle2TextMapHash;
	public uint storyContext2TextMapHash;
	public uint[] tips;
	public uint storyTitleLockedTextMapHash;

	public static async Task Load<Key>(string path, Func<FetterStoryData, Key> keySelector, IDictionary<Key, FetterStoryData> map)
	{
		using StringReader? sr = new(await File.ReadAllTextAsync(path));
		//skip header
		sr.ReadLine();
		string? line;
		while ((line = sr.ReadLine()) != null)
		{
			if (string.IsNullOrEmpty(line)) continue;
			FetterStoryData obj = ParseLine(line);
			map.Add(keySelector(obj), obj);
		}
	}

	private static FetterStoryData ParseLine(string line)
	{
		FetterStoryData obj = new();
		ReadOnlySpan<char> span = line.AsSpan();
		int t01 = span.IndexOf('\t');
		obj.fetter_id = uint.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.avatarId = uint.Parse(span[..t01]);
		//openConds
		obj.openConds = new FetterConditionConfig[] { new(), new() };
		for (int i = 0; i < 2; i++)
		{
			span = span[(t01 + 1)..];
			t01 = span.IndexOf('\t');
			if (!span[..t01].IsEmpty)
				obj.openConds[i].condType = (FetterCondType)ulong.Parse(span[..t01]);
			span = span[(t01 + 1)..];
			t01 = span.IndexOf('\t');
			//array
			ReadOnlySpan<char> openarr = span[..t01];
			List<uint> openparamList = new();
			while (true)
			{
				int tarr = openarr.IndexOf(';');
				if (tarr == -1)
				{
					if (!openarr.IsEmpty)
						openparamList.Add(uint.Parse(openarr));
					break;
				}
				else
				{
					openparamList.Add(uint.Parse(openarr[..tarr]));
					openarr = openarr[(tarr + 1)..];
				}
			}
			obj.openConds[i].paramList = openparamList.ToArray();
			//array
		}
		//openConds
		//finishConds
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.finishConds = new FetterConditionConfig[] { new() };
		if (!span[..t01].IsEmpty)
			obj.finishConds[0].condType = (FetterCondType)ulong.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		//array		
		List<uint> paramList = new();
		ReadOnlySpan<char> arr = span;
		while (true)
		{
			int tarr = arr.IndexOf(';');
			if (tarr == -1)
			{
				if (!arr.IsEmpty)
					paramList.Add(uint.Parse(arr));
				break;
			}
			else
			{
				paramList.Add(uint.Parse(arr[..tarr]));
				arr = arr[(tarr + 1)..];
			}
		}
		END:
		obj.finishConds[0].paramList = paramList.ToArray();
		//array
		//finishConds
		return obj;
	}
}
