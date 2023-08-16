using Google.Protobuf;
using Weedwacker.Shared.Network.Proto;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Packet.Send
{
    internal class PacketPlayerLoginRsp : BasePacket
    {
        private static QueryCurrRegionHttpRsp RegionCache;

        public PacketPlayerLoginRsp(Connection session) : base(Enums.OpCode.PlayerLoginRsp, 1)
        {
            RegionInfo info;


            if (RegionCache == null)
            {
                try
                {
                    // todo: we might want to push custom config to client
                    RegionInfo serverRegion = new()
                    {
                        GateserverIp = GameServer.Configuration.Server.AccessAddress,
                        GateserverPort = (uint)GameServer.Configuration.Server.AccessPort,
                        SecretKey = ByteString.CopyFrom(Crypto.DISPATCH_SEED),
                        ResVersionConfig = new ResVersionConfig(),
                    };
                    RegionCache = new QueryCurrRegionHttpRsp()
                    {
                        RegionInfo = serverRegion
                    };
                }
                catch (Exception e)
                {
                    Logger.WriteErrorLine("Error while initializing region cache!", e);
                }
            }

            info = RegionCache.RegionInfo;

            PlayerLoginRsp p = new()
            {
                IsUseAbilityHash = true,
                AbilityHashCode = 1844674,
                GameBiz = "hk4e_global",
                ClientDataVersion = info.ClientDataVersion,
                ClientSilenceDataVersion = info.ClientSilenceDataVersion,
                ClientMd5 = info.ClientDataMd5,
                ClientSilenceMd5 = info.ClientSilenceDataMd5,
                ResVersionConfig = info.ResVersionConfig,
                ClientVersionSuffix = info.ClientVersionSuffix,
                ClientSilenceVersionSuffix = info.ClientSilenceVersionSuffix,
                IsScOpen = false,
                RegisterCps = "mihoyo",
                CountryCode = "US"
            };

            Data = p.ToByteArray();
        }

    }
}
