using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class FettersData : FetterConfig
{
	public uint type;
	public bool isHiden;
	public uint[] hideCostumeList;
	public uint[] showCostumeList;
	public uint[] tips;
	public uint voice_titleTextMapHash;
	public string voice_file;
	public uint voice_file_textTextMapHash;
	public uint voice_title_lockedTextMapHash;

	public static async Task Load<Key>(string path, Func<FettersData, Key> keySelector, IDictionary<Key, FettersData> map)
	{
		using StringReader? sr = new(await File.ReadAllTextAsync(path));
		//skip header
		sr.ReadLine();
		string? line;
		while ((line = sr.ReadLine()) != null)
		{
			if (string.IsNullOrEmpty(line)) continue;
			FettersData obj = ParseLine(line);
			map.Add(keySelector(obj), obj);
		}
	}

	private static FettersData ParseLine(string line)
	{
		FettersData obj = new();
		ReadOnlySpan<char> span = line.AsSpan();
		int t01 = span.IndexOf('\t');
		obj.fetter_id = uint.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.avatarId = uint.Parse(span[..t01]);
		//openConds
		obj.openConds = new FetterConditionConfig[] { new(), new(), new(), new() };
		for (int i = 0; i < 4; i++)
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
		ReadOnlySpan<char> arr = span[..t01];
		List<uint> paramList = new();
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
		obj.finishConds[0].paramList = paramList.ToArray();
		//array
		//finishConds
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		//hide array
		ReadOnlySpan<char> hidearr = span[..t01];
		List<uint> hideList = new();
		while (true)
		{
			int tarr = hidearr.IndexOf(';');
			if (tarr == -1)
			{
				if (!hidearr.IsEmpty)
					hideList.Add(uint.Parse(hidearr));
				break;
			}
			else
			{
				hideList.Add(uint.Parse(hidearr[..tarr]));
				hidearr = hidearr[(tarr + 1)..];
			}
		}
		obj.hideCostumeList = hideList.ToArray();
		//hide array
		span = span[(t01 + 1)..];
		//show array
		ReadOnlySpan<char> showarr = span;
		List<uint> showList = new();
		while (true)
		{
			int tarr = showarr.IndexOf(';');
			if (tarr == -1)
			{
				if (!showarr.IsEmpty)
					showList.Add(uint.Parse(showarr));
				break;
			}
			else
			{
				showList.Add(uint.Parse(showarr[..tarr]));
				showarr = showarr[(tarr + 1)..];
			}
		}
		obj.hideCostumeList = showList.ToArray();
		//show array
		return obj;
	}
}
