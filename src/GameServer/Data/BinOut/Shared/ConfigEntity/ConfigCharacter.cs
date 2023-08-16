
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Weedwacker.GameServer.Data.BinOut.Shared.ConfigEntity;

public class ConfigCharacter : ConfigEntity
{
	public ConfigCombat combat;
	public ConfigEquipController equipController;
	public ConfigSubEquipController[] subEquipControllers;
	[JsonProperty("abilities")]public /*ConfigEntityAbilityEntry[]*/object _abilities; //it may appear as an empty {} NOT [] for some god forsaken reason
	public Dictionary<string, ConfigBaseStateLayer> stateLayers;
	public ConfigFace face;
	public ConfigPartController partControl;
	public ConfigBillboard billboard;
	public string[] bindEmotions;

	[JsonIgnore] public ConfigEntityAbilityEntry[]? abilities;

	[OnDeserialized]
	public void foo(StreamingContext context)
	{
		if (_abilities == null) return;
		Type wtfisthis = _abilities.GetType();
		if(_abilities is JArray arr)
		{
			abilities = arr.ToObject<ConfigEntityAbilityEntry[]>(GameData.Serializer);
		}
		else if(_abilities is JObject obj)
		{
			ConfigEntityAbilityEntry? entry = obj.ToObject<ConfigEntityAbilityEntry>(GameData.Serializer);
			if (entry != null && (entry.abilityID != null || entry.abilityName != null))
				abilities = new[] { entry };
			else
				abilities = null;
		}
	}
	//private ConfigEntityAbilityEntry[] Resolve()
	//{
	//	if (_abilities.GetType().IsArray)
	//	{
	//		return GameData.Serializer.Deserialize(_abilities);
	//	}
	//}
}