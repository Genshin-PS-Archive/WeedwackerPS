using Google.Protobuf;
using Weedwacker.GameServer.Systems.Ability;
using Weedwacker.GameServer.Systems.World;

namespace Weedwacker.GameServer.Data.BinOut.Ability.Temp;

public interface IInvocation
{
    public Task Invoke(AbilityInstance ability, ByteString abilityData, BaseEntity? targetEntity);
}
