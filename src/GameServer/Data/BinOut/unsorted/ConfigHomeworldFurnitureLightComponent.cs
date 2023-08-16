using Weedwacker.GameServer.Data.BinOut.Shared;

namespace Weedwacker.GameServer.Data;

public class ConfigHomeworldFurnitureLightComponent
{
	public float lightRange;
	public float lightIntensity;
	public float minViewDistRatio;
	public float maxViewDistRatio;
	public uint lightType;
	public uint lightmapBakeType;
	public uint lightShadingMode;
	public ColorVector lightColor;
	public Vector lightHSV;
}