﻿namespace Weedwacker.GameServer.Data.BinOut.Ability.Temp.Actions;

public class SetGlobalValue : ConfigAbilityAction
{
	public object value;
	public string key;
	public bool useLimitRange;
	public bool randomInRange;
	public object maxValue;
	public object minValue;
}
