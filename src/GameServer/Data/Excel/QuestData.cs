using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class QuestData
{
	public uint subId;
	public uint mainId;
	public int order;
	public uint? subIdSet;
	public bool isMpBlock;
	public uint descTextMapHash;
	public uint stepDescTextMapHash;
	public uint guideTipsTextMapHash;
	public QuestShowType? showType;
	public BanGroupType banType;
	public LogicType? acceptCondComb;
	public QuestCond[] acceptCond;
	public LogicType? finishCondComb;
	public QuestContent[] finishCond;
	public LogicType failCondComb;
	public QuestContent[] failCond;
	public QuestGuide guide;
	public ShowQuestGuideType? showGuide;
	public bool finishParent;
	public bool failParent;
	public QuestShowType failParentShow;
	public bool isRewind;
	public QuestExec[] finishExec;
	public QuestExec[] failExec;
	public QuestExec[] beginExec;
	public uint[] exclusiveNpcList;
	public uint[] sharedNpcList;
	public uint? exclusiveNpcPriority;
	public uint[] trialAvatarList;
	public uint[] exclusivePlaceList;

	//not in client----------//
	public QuestRewards questRewardItems;
	public string loadAbilityGroup;
	public uint[] CoopPointID;
	public bool isRefreshLimitedToStandAlone;
	public string levelMapping;
	public class QuestRewards
	{
		public uint[] id;
		public uint[] count;
	}
	//-----------------------//
	internal static async Task Load<Key>(string path, Func<QuestData, Key> keySelector, IDictionary<Key, QuestData> map, QuestDataLoadContext ctx)
	{
		using StringReader? sr = new(await File.ReadAllTextAsync(path));
		//skip header
		sr.ReadLine();
		string? line;
		while ((line = sr.ReadLine()) != null)
		{
			if (string.IsNullOrEmpty(line)) continue;
			QuestData obj = ParseLine(line, ctx);
			map.Add(keySelector(obj), obj);
		}
	}

	private static QuestData ParseLine(string line, QuestDataLoadContext ctx)
	{
		QuestData obj = new();
		ReadOnlySpan<char> span = line.AsSpan();
		int t01 = span.IndexOf('\t');
		obj.subId = uint.Parse(span[..t01]); //0
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.mainId = uint.Parse(span[..t01]); //1
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.order = int.Parse(span[..t01]); //2
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.subIdSet = uint.Parse(span[..t01]); //3
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.isMpBlock = !span[..t01].IsEmpty && span[0] == '1'; //4
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.showType = (QuestShowType)ulong.Parse(span[..t01]); //5
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.acceptCondComb = (LogicType)ulong.Parse(span[..t01]); //6
		obj.acceptCond = new QuestCond[ctx.AcceptCondCount];
		for (int i = 0; i < ctx.AcceptCondCount; i++)
		{
			obj.acceptCond[i] = new();
			obj.acceptCond[i].param = new int?[ctx.AcceptCondParamCount];
			span = span[(t01 + 1)..];
			t01 = span.IndexOf('\t');
			if (!span[..t01].IsEmpty)
				obj.acceptCond[i].type = (QuestCondType)ulong.Parse(span[..t01]);
			for (int j = 0; j < ctx.AcceptCondParamCount; j++)
			{
				span = span[(t01 + 1)..];
				t01 = span.IndexOf('\t');
				if (!span[..t01].IsEmpty)
					obj.acceptCond[i].param[j] = int.Parse(span[..t01]);
			}
		}
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.finishCondComb = (LogicType)ulong.Parse(span[..t01]);
		obj.finishCond = new QuestContent[ctx.FinishCondParamCount.Length];
		for (int i = 0; i < ctx.FinishCondParamCount.Length; i++)
		{
			obj.finishCond[i] = new();
			obj.finishCond[i].param = new int?[ctx.FinishCondParamCount[i]];
			span = span[(t01 + 1)..];
			t01 = span.IndexOf('\t');
			if (!span[..t01].IsEmpty)
				obj.finishCond[i].type = (QuestContentType)ulong.Parse(span[..t01]);
			for (int j = 0; j < ctx.FinishCondParamCount[i]; j++)
			{
				span = span[(t01 + 1)..];
				t01 = span.IndexOf('\t');
				if (!span[..t01].IsEmpty)
					obj.finishCond[i].param[j] = int.Parse(span[..t01]);
			}
			span = span[(t01 + 1)..];
			t01 = span.IndexOf('\t');
			obj.finishCond[i].param_str = span[..t01].ToString();
			span = span[(t01 + 1)..];
			t01 = span.IndexOf('\t');
			if (!span[..t01].IsEmpty)
				obj.finishCond[i].count = uint.Parse(span[..t01]);
		}
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.failCondComb = (LogicType)ulong.Parse(span[..t01]);
		obj.failCond = new QuestContent[ctx.FailCondCount];
		for (int i = 0; i < ctx.FailCondCount; i++)
		{
			obj.failCond[i] = new();
			obj.failCond[i].param = new int?[ctx.FailCondParamCount];
			span = span[(t01 + 1)..];
			t01 = span.IndexOf('\t');
			if (!span[..t01].IsEmpty)
				obj.failCond[i].type = (QuestContentType)ulong.Parse(span[..t01]);
			for (int j = 0; j < ctx.FailCondParamCount; j++)
			{
				span = span[(t01 + 1)..];
				t01 = span.IndexOf('\t');
				if (!span[..t01].IsEmpty)
					obj.failCond[i].param[j] = int.Parse(span[..t01]);
			}
			span = span[(t01 + 1)..];
			t01 = span.IndexOf('\t');
			obj.failCond[i].param_str = span[..t01].ToString();
			span = span[(t01 + 1)..];
			t01 = span.IndexOf('\t');
			if (!span[..t01].IsEmpty)
				obj.failCond[i].count = uint.Parse(span[..t01]);
		}
		obj.guide = new();
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.guide.type = (QuestGuideType)ulong.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.guide.autoGuide = (QuestGuideAuto)ulong.Parse(span[..t01]);
		obj.guide.param = new string[ctx.GuideParamCount];
		for (int i = 0; i < ctx.GuideParamCount; i++)
		{
			span = span[(t01 + 1)..];
			t01 = span.IndexOf('\t');
			obj.guide.param[i] = span[..t01].ToString();
		}
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.guide.guideScene = uint.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.guide.guideStyle = (QuestGuideStyle)ulong.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.guide.guideLayer = (QuestGuideLayer)ulong.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.showGuide = (ShowQuestGuideType)ulong.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.finishParent = !span[..t01].IsEmpty && span[0] == '1';
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.failParent = !span[..t01].IsEmpty && span[0] == '1';
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.isRewind = !span[..t01].IsEmpty && span[0] == '1';
		obj.questRewardItems = new();
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');


		ReadOnlySpan<char> spanToUse = span[..t01];
		List<uint> uintList = new();


		if (!span[..t01].IsEmpty)
		{
			while (true)
			{
				int tarr = spanToUse.IndexOf(',');
				if (tarr == -1)
				{
					if (!spanToUse.IsEmpty)
						uintList.Add(uint.Parse(spanToUse));
					break;
				}
				uintList.Add(uint.Parse(spanToUse[..tarr]));
				spanToUse = spanToUse[(tarr + 1)..];
			}
			obj.questRewardItems.id = uintList.ToArray();
		}
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');

		spanToUse = span[..t01];
		uintList.Clear();
		if (!span[..t01].IsEmpty)
		{
			while (true)
			{
				int tarr = spanToUse.IndexOf(',');
				if (tarr == -1)
				{
					if (!spanToUse.IsEmpty)
						uintList.Add(uint.Parse(spanToUse));
					break;
				}
				uintList.Add(uint.Parse(spanToUse[..tarr]));
				spanToUse = spanToUse[(tarr + 1)..];
			}
			obj.questRewardItems.count = uintList.ToArray();
		}
		obj.finishExec = new QuestExec[ctx.FinishExecCount];
		for (int i = 0; i < ctx.FinishExecCount; i++)
		{
			obj.finishExec[i] = new();
			obj.finishExec[i].param = new string[ctx.FinishExecParamCount];
			span = span[(t01 + 1)..];
			t01 = span.IndexOf('\t');
			if (!span[..t01].IsEmpty)
				obj.finishExec[i].type = (QuestExecType)ulong.Parse(span[..t01]);
			for (int j = 0; j < ctx.FinishExecParamCount; j++)
			{
				span = span[(t01 + 1)..];
				t01 = span.IndexOf('\t');
				obj.finishExec[i].param[j] = span[..t01].ToString();
			}
		}
		obj.failExec = new QuestExec[ctx.FailExecCount];
		for (int i = 0; i < ctx.FailExecCount; i++)
		{
			obj.failExec[i] = new();
			obj.failExec[i].param = new string[ctx.FailExecParamCount];
			span = span[(t01 + 1)..];
			t01 = span.IndexOf('\t');
			if (!span[..t01].IsEmpty)
				obj.failExec[i].type = (QuestExecType)ulong.Parse(span[..t01]);
			for (int j = 0; j < ctx.FailExecParamCount; j++)
			{
				span = span[(t01 + 1)..];
				t01 = span.IndexOf('\t');
				obj.failExec[i].param[j] = span[..t01].ToString();
			}
		}
		obj.beginExec = new QuestExec[ctx.BeginExecCount];
		for (int i = 0; i < ctx.BeginExecCount; i++)
		{
			obj.beginExec[i] = new();
			obj.beginExec[i].param = new string[ctx.BeginExecParamCount];
			span = span[(t01 + 1)..];
			t01 = span.IndexOf('\t');
			if (!span[..t01].IsEmpty)
				obj.beginExec[i].type = (QuestExecType)ulong.Parse(span[..t01]);
			for (int j = 0; j < ctx.BeginExecParamCount; j++)
			{
				span = span[(t01 + 1)..];
				t01 = span.IndexOf('\t');
				obj.beginExec[i].param[j] = span[..t01].ToString();
			}
		}
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		spanToUse = span[..t01];
		uintList.Clear();
		while(true)
		{
			int tarr = spanToUse.IndexOf(',');
			if (tarr == -1)
			{
				if (!spanToUse.IsEmpty)
					uintList.Add(uint.Parse(spanToUse));
				break;
			}
			uintList.Add(uint.Parse(spanToUse[..tarr]));
			spanToUse = spanToUse[(tarr + 1)..];
		}
		obj.exclusiveNpcList = uintList.ToArray();
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		spanToUse = span[..t01];
		uintList.Clear();
		while (true)
		{
			int tarr = spanToUse.IndexOf(',');
			if (tarr == -1)
			{
				if (!spanToUse.IsEmpty)
					uintList.Add(uint.Parse(spanToUse));
				break;
			}
			uintList.Add(uint.Parse(spanToUse[..tarr]));
			spanToUse = spanToUse[(tarr + 1)..];
		}
		obj.sharedNpcList = uintList.ToArray();
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		if (!span[..t01].IsEmpty)
			obj.exclusiveNpcPriority = uint.Parse(span[..t01]);
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		spanToUse = span[..t01];
		uintList.Clear();
		while (true)
		{
			int tarr = spanToUse.IndexOf(',');
			if (tarr == -1)
			{
				if (!spanToUse.IsEmpty)
					uintList.Add(uint.Parse(spanToUse));
				break;
			}
			uintList.Add(uint.Parse(spanToUse[..tarr]));
			spanToUse = spanToUse[(tarr + 1)..];
		}
		obj.trialAvatarList = uintList.ToArray();
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.loadAbilityGroup = span[..t01].ToString();
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		spanToUse = span[..t01];
		uintList.Clear();
		while (true)
		{
			int tarr = spanToUse.IndexOf(',');
			if (tarr == -1)
			{
				if (!spanToUse.IsEmpty)
					uintList.Add(uint.Parse(spanToUse));
				break;
			}
			uintList.Add(uint.Parse(spanToUse[..tarr]));
			spanToUse = spanToUse[(tarr + 1)..];
		}
		obj.exclusivePlaceList = uintList.ToArray();
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		spanToUse = span[..t01];
		uintList.Clear();
		if (!span[..t01].IsEmpty)
		{
			while (true)
			{
				int tarr = spanToUse.IndexOf(',');
				if (tarr == -1)
				{
					if (!spanToUse.IsEmpty)
						uintList.Add(uint.Parse(spanToUse));
					break;
				}
				uintList.Add(uint.Parse(spanToUse[..tarr]));
				spanToUse = spanToUse[(tarr + 1)..];
			}
			obj.CoopPointID = uintList.ToArray();
		}
		span = span[(t01 + 1)..];
		t01 = span.IndexOf('\t');
		obj.isRefreshLimitedToStandAlone = !span[..t01].IsEmpty && span[0] == '1';
		if (span.Length > t01 + 1 + 1)
			obj.levelMapping = span[(t01 + 1)..].ToString();
		return obj;
	}
}


internal class QuestDataLoadContext
{
	public uint GuideParamCount, FinishExecCount, AcceptCondParamCount, AcceptCondCount,
		FailCondParamCount, FailCondCount, FinishExecParamCount, FailExecCount,
		FailExecParamCount, BeginExecCount, BeginExecParamCount;
	public uint FinishCondsWithOneParam;
	public bool hasAutoGuide;
	public uint[] FinishCondParamCount;
}