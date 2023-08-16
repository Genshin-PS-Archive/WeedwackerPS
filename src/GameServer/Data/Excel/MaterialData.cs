using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(0)] //hack
public class MaterialData : ItemConfig
{
	public uint interactionTitleTextMapHash;
	public MaterialType? materialType;
	public uint? cdTime;
	public uint? cdGroup;
	public uint? stackLimit;
	public uint? maxUseCount;
	public bool useOnGain;
	public bool noFirstGetHint;
	public bool playGainEffect;
	public ItemUseTarget? useTarget;
	public ItemUseConfig[] itemUse;
	public uint? rankLevel;
	public FoodQualityType foodQuality;
	public uint effectDescTextMapHash;
	public uint specialDescTextMapHash;
	public uint typeDescTextMapHash;
	public string effectIcon;
	public uint effectGadgetID;
	public string effectName;
	public string[] picPath;
	public bool isSplitDrop;
	public bool closeBagAfterUsed;
	public uint[] satiationParams;
	public MaterialDestroyType? destroyRule;
	public uint[] destroyReturnMaterial;
	public uint[] destroyReturnMaterialCount;
	public uint? setID;
	public bool isHidden;
	public bool? isForceGetHint;

	public class ItemUseConfig
	{
		public ItemUseOp? useOp;
		public string?[] useParam;
	}

	public static async Task Load<Key>(string path, Func<MaterialData, Key> keySelector, IDictionary<Key, ItemConfig> map)
	{
		using StringReader? sr = new(await File.ReadAllTextAsync(path));
		//skip header
		sr.ReadLine();
		string? line;
		while ((line = sr.ReadLine()) != null)
		{
			if (string.IsNullOrEmpty(line)) continue;
			MaterialData obj = ParseLine(line);
			map.Add(keySelector(obj), obj);
		}
	}

	private static MaterialData ParseLine(string line)
	{
		MaterialData obj = new();
		ReadOnlySpan<char> span = line.AsSpan();
		obj = TsvParser.DeserializeSpan<MaterialData>(ref span);
		int t01 = span.IndexOf('\t');
		if(!span[..t01].IsEmpty)
			obj.materialType = (MaterialType)ulong.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.cdTime = uint.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.cdGroup = uint.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.stackLimit = uint.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.maxUseCount = uint.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.useOnGain = span[0] == '1';
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.noFirstGetHint = span[0] == '1';
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.useTarget = (ItemUseTarget)ulong.Parse(span[..t01]);
		//itemUse
		obj.itemUse = new ItemUseConfig[] { new(), new() };
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.itemUse[0].useOp = (ItemUseOp)ulong.Parse(span[..t01]);
		obj.itemUse[0].useParam = new string[5];
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.itemUse[0].useParam[0] = span[..t01].ToString();
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.itemUse[0].useParam[1] = span[..t01].ToString();
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.itemUse[0].useParam[2] = span[..t01].ToString();
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.itemUse[0].useParam[3] = span[..t01].ToString();
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.itemUse[0].useParam[4] = span[..t01].ToString();
		obj.itemUse[1].useParam = new string[2];
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.itemUse[1].useOp = (ItemUseOp)ulong.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.itemUse[1].useParam[0] = span[..t01].ToString();
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.itemUse[1].useParam[1] = span[..t01].ToString();
		//itemUse
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.rankLevel = uint.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.closeBagAfterUsed = span[0] == '1';
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.isSplitDrop = span[0] == '1';
		//array
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		List<uint> paramList = new();
		ReadOnlySpan<char> arr = span[..t01];
		while (true)
		{
			int tarr = arr.IndexOf(',');
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
		obj.satiationParams = paramList.ToArray();
		//array
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.destroyRule = (MaterialDestroyType)ulong.Parse(span[..t01]);
		//array
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		paramList.Clear();
		arr = span[..t01];
		while (true)
		{
			int tarr = arr.IndexOf(',');
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
		obj.destroyReturnMaterial = paramList.ToArray();
		//array
		//array
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		paramList.Clear();
		arr = span[..t01];
		while (true)
		{
			int tarr = arr.IndexOf(',');
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
		obj.destroyReturnMaterialCount = paramList.ToArray();
		//array
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.setID = uint.Parse(span[..t01]);
		if (!span[(t01 + 1)..].IsEmpty)
			obj.isForceGetHint = span[t01 + 1] == '1';

		return obj;
	}
}
