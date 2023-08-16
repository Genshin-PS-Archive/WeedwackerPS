using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Data
{
	internal static partial class DynamicFloatHelper
	{

		public static float ResolveDynamicFloat(object dynFloat, BaseEntity entity, string configAbilityName)
		{
			uint ability = Utils.AbilityHash(configAbilityName);
			return ResolveDynamicFloat(dynFloat, entity, ability);
		}
		public static float ResolveDynamicFloat(object dynFloat, BaseEntity entity, uint configAbilityHash)
		{
			if (dynFloat is double asDouble)
				return (float)asDouble;
			else if (dynFloat is float asFloat)
				return asFloat;
			else if (dynFloat is string)
			{
				uint ability = configAbilityHash;
				uint special = Utils.AbilityHash(dynFloat as string);
				if (entity.AbilityManager.AbilitySpecialOverrideMap[ability].ContainsKey(special))
					return entity.AbilityManager.AbilitySpecialOverrideMap[ability][special];
				else // eg %X+%Y
					return ResolvePercentageSymbolReferences(dynFloat as string, entity, configAbilityHash);
			}
			else if (dynFloat is long lgon)
			{
				return lgon;
			}
			else if (dynFloat is JArray expression)
			{
				return ResolvePolishNotation(expression.ToObject<string[]>(), entity, configAbilityHash);
			}
			else
			{
				Logger.WriteErrorLine($"Unknown dynamic float expression: {dynFloat}, {dynFloat.GetType()}");
				return 0;
			}
		}

		private static float ResolvePolishNotation(object[] expression, BaseEntity entity, uint configAbilityHash)
		{
			Stack<float> myStack = new();
			float value;
			uint ability = configAbilityHash;

			for (int i = 0; i < expression.Length; i++)
			{
				if (expression[i] is float asFloat)
					myStack.Push(asFloat);
				else
				{
					float local;
					switch (expression[i] as string)
					{
						case "Add":
							local = myStack.Pop() + myStack.Pop();
							myStack.Push(local);
							break;
						case "Mul":
							local = myStack.Pop() * myStack.Pop();
							myStack.Push(local);
							break;
						default:
							uint special = Utils.AbilityHash(expression[i] as string);
							if (entity.AbilityManager.AbilitySpecialOverrideMap[ability].TryGetValue(special, out float fVal))
							{
								myStack.Push(fVal);
								break;
							}
							else if (float.TryParse(expression[i] as string, out float floatyFloat))
							{
								myStack.Push(floatyFloat);
								break;
							}
							else if (entity is SceneEntity sceneEntity && sceneEntity.FightProps != null && sceneEntity.FightProps.TryGetValue((FightPropType)Enum.Parse(typeof(FightPropType), expression[i] as string), out float fightProp))
							{
								myStack.Push(fightProp);
								break;
							}
							else
							{
								Logger.WriteErrorLine($"Unknown dynamic float expression: {expression}");
								return 0;
							}
					}
				}
			}
			return myStack.Pop();
		}

		private static float ResolvePercentageSymbolReferences(string dynFloat, BaseEntity entity, uint configAbilityHash)
		{
			uint ability = configAbilityHash;
			MatchCollection? specials = PercentRef().Matches(dynFloat); // match "%Word"
			foreach (Match match in specials.Cast<Match>())
			{
				string withoutPerc = match.Value.Replace("%", string.Empty);
				uint special = Utils.AbilityHash(withoutPerc);
				if (entity.AbilityManager.AbilitySpecialOverrideMap[ability].TryGetValue(special, out float fVal))
					dynFloat = Regex.Replace(dynFloat, match.Value, fVal.ToString(CultureInfo.InvariantCulture));
				else if (entity is SceneEntity sceneEntity && sceneEntity.FightProps.TryGetValue((FightPropType)Enum.Parse(typeof(FightPropType), match.Value), out float fightProp))
					dynFloat = Regex.Replace(dynFloat, match.Value, fightProp.ToString(CultureInfo.InvariantCulture));
				else
				{
					Logger.WriteErrorLine("failed to dereference PercentageSymbolReference");
					return 0;
				}
			}
			DataTable dt = new();
			return (float)(decimal)dt.Compute(dynFloat, "");
		}

		[GeneratedRegex("(?<!\\w)%\\w+")]
		private static partial Regex PercentRef();
	}
}