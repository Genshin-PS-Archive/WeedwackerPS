using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace Weedwacker.GameServer.Data;

// To handle $type fields
public class KnownTypesBinder : ISerializationBinder
{
    private static readonly Dictionary<string, Type> KnownTypes;

	public Type BindToType(string? assemblyName, string typeName)
	{
		return KnownTypes.GetValueOrDefault(typeName);
	}

	public void BindToName(Type serializedType, out string assemblyName, out string typeName)
	{
		assemblyName = null;
		typeName = serializedType.Name;
	}

	static KnownTypesBinder()
	{
		KnownTypes = new Dictionary<string, Type>();
		const string data = "Weedwacker.GameServer.Data";
		IEnumerable<Type>? types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace != null && t.Namespace.StartsWith(data));
		foreach (Type type in types)
		{
			if (type.Name.Contains("<")) continue;
			KnownTypes.Add(type.Name, type);
		}
	}
}

