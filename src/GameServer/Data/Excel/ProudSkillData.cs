using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class ProudSkillData : BaseTalentConfig
{
    public uint proudSkillId;
    public uint proudSkillGroupId;
    public uint level;
    public uint proudSkillType;
    public uint nameTextMapHash;
    public uint descTextMapHash;
    public uint unlockDescTextMapHash;
    public string icon;
    public uint coinCost;
    public IdCountConfig[] costItems;
    public TalentFilterCond?[] filterConds;
    public uint breakLevel;
    public uint[] paramDescList;
    public ProudLifeEffectType? lifeEffectType;
    public string[] lifeEffectParams;
    public byte? effectiveForTeam;
    public bool? isHideLifeProudSkill;

    public IEnumerable<IdCountConfig> GetTotalCostItems()
    {
        List<IdCountConfig> total = new();
        total = (List<IdCountConfig>)total.Concat(costItems);
        if (coinCost > 0)
            total.Add(new IdCountConfig { id = 202, count = coinCost });

        return total;
    }

    public static async Task Load<Key>(string path, Func<ProudSkillData, Key> keySelector, IDictionary<Key, ProudSkillData> map)
    {
        using StringReader? sr = new(await File.ReadAllTextAsync(path));
        //skip header
        sr.ReadLine();
        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            if (string.IsNullOrEmpty(line)) continue;
            ProudSkillData obj = ParseLine(line);
            map.Add(keySelector(obj), obj);
        }
    }

    private static ProudSkillData ParseLine(string line)
    {
        ProudSkillData obj = new();
        ReadOnlySpan<char> span = line.AsSpan();
        int t01 = span.IndexOf('\t');
        obj.proudSkillId = uint.Parse(span[..t01]);
        span = span[(t01 + 1)..];
        t01 = span.IndexOf('\t');
        obj.openConfig = span[..t01].ToString();
        obj.addProps = new PropValConfig[] { new(), new() };
        span = span[(t01 + 1)..];
        t01 = span.IndexOf('\t');
        if (t01 != 0)
            obj.addProps[0].propType = (FightPropType)ulong.Parse(span[..t01]);
        span = span[(t01 + 1)..];
        t01 = span.IndexOf('\t');
        if (t01 != 0)
            obj.addProps[0].value = float.Parse(span[..t01]);
        span = span[(t01 + 1)..];
        t01 = span.IndexOf('\t');
        if (t01 != 0)
            obj.addProps[1].propType = (FightPropType)ulong.Parse(span[..t01]);
        span = span[(t01 + 1)..];
        t01 = span.IndexOf('\t');
        if (t01 != 0)
            obj.addProps[1].value = float.Parse(span[..t01]);
        obj.paramList = new float?[20];
        for (int i = 0; i < 20; i++)
        {
            span = span[(t01 + 1)..];
            t01 = span.IndexOf('\t');
            if (t01 != 0)
                obj.paramList[i] = float.Parse(span[..t01]);
        }
        span = span[(t01 + 1)..];
        t01 = span.IndexOf('\t');
        obj.proudSkillGroupId = uint.Parse(span[..t01]);
        span = span[(t01 + 1)..];
        t01 = span.IndexOf('\t');
        obj.level = uint.Parse(span[..t01]);
        span = span[(t01 + 1)..];
        t01 = span.IndexOf('\t');
        obj.proudSkillType = uint.Parse(span[..t01]);
        span = span[(t01 + 1)..];
        t01 = span.IndexOf('\t');
        obj.coinCost = uint.Parse(span[..t01]);
        obj.costItems = new IdCountConfig[] { new(), new(), new(), new() };
        for (int i = 0; i < 4; i++)
        {
            span = span[(t01 + 1)..];
            t01 = span.IndexOf('\t');
            obj.costItems[i].id = uint.Parse(span[..t01]);
            span = span[(t01 + 1)..];
            t01 = span.IndexOf('\t');
            obj.costItems[i].count = uint.Parse(span[..t01]);
        }
        obj.filterConds = new TalentFilterCond?[2];
        span = span[(t01 + 1)..];
        t01 = span.IndexOf('\t');
        if (t01 != 0)
            obj.filterConds[0] = (TalentFilterCond)ulong.Parse(span[..t01]);
        span = span[(t01 + 1)..];
        t01 = span.IndexOf('\t');
        if (t01 != 0)
            obj.filterConds[1] = (TalentFilterCond)ulong.Parse(span[..t01]);
        span = span[(t01 + 1)..];
        t01 = span.IndexOf('\t');
        obj.breakLevel = uint.Parse(span[..t01]);
        span = span[(t01 + 1)..];
        t01 = span.IndexOf('\t');
        if (t01 != 0)
            obj.lifeEffectType = (ProudLifeEffectType)ulong.Parse(span[..t01]);
        obj.lifeEffectParams = new string[5];
        for (int i = 0; i < 5; i++)
        {
            span = span[(t01 + 1)..];
            t01 = span.IndexOf('\t');
            obj.lifeEffectParams[i] = span[..t01].ToString();
        }
        span = span[(t01 + 1)..];
        t01 = span.IndexOf('\t');
        if (t01 != 0)
            obj.effectiveForTeam = byte.Parse(span[..t01]);
        if (span.Length > t01 + 1 + 1)
            obj.isHideLifeProudSkill = span[t01 + 1] == '1';
        return obj;
    }
}
