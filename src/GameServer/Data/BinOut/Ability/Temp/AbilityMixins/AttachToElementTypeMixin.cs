﻿using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.BinOut.Ability.Temp.AbilityMixins;

public class AttachToElementTypeMixin : ConfigAbilityMixin
{
	public ElementType[] elementTypes;
	public bool reject;
	public string modifierName;
}