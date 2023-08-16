using Weedwacker.GameServer.Data.BinOut.Shared;

namespace Weedwacker.GameServer.Data;

public class ConfigSpecialElementViewAction : ConfigBaseInterAction
{
	public bool isOpen;
	public ColorVector effectColor;
	public float effectRange;
	public string[] noEffectAliasList;
}