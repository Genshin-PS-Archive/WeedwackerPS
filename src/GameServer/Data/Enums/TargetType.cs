
namespace Weedwacker.GameServer.Data.Enums;

public enum TargetType : int
{
	None = 0,
	Alliance = 1,
	Enemy = 2,
	Self = 3,
	SelfCamp = 4,
	All = 5,
	AllExceptSelf = 6,
	AllianceIncludeSelf,
}