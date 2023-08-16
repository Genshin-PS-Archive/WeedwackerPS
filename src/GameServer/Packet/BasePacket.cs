using Google.Protobuf;
using Weedwacker.GameServer.Enums;
using Weedwacker.Shared.Network.Proto;


namespace Weedwacker.GameServer.Packet
{
    internal class BasePacket
    {
        private const ushort Magic1 = 0x4567;
        private const ushort Magic2 = 0x89ab;

        public ushort Opcode { get; protected set; }
        public bool ShouldBuildHeader { get; protected set; } = false;

        private byte[] Header = Array.Empty<byte>();
        public byte[] Data { get; protected set; } = Array.Empty<byte>();

        public BasePacket(OpCode opcode)
        {
            Opcode = (ushort)opcode;
        }

        public BasePacket(OpCode opcode, uint clientSequence)
        {
            Opcode = (ushort)opcode;
            BuildHeader(clientSequence);
        }

        public BasePacket(OpCode opcode, bool buildHeader)
        {
            Opcode = (ushort)opcode;
            ShouldBuildHeader = buildHeader;
        }

        public BasePacket BuildHeader(uint clientSequence)
        {
            if (clientSequence == 0)
            {
                return this;
            }
            PacketHead? header = new()
            {
                ClientSequenceId = clientSequence,
                SentMs = (ulong)DateTimeOffset.Now.ToUnixTimeMilliseconds()
            };

            Header = header.ToByteArray();
            return this;
        }

        public async Task<byte[]> BuildPacketAsync()
        {
            await using MemoryStream? ms = new();
            using BinaryWriter? bw = new(ms);

            bw.WriteUInt16BE(Magic1);
            bw.WriteUInt16BE(Opcode);
            bw.WriteUInt16BE((ushort)Header.Length);
            bw.WriteUInt32BE((uint)Data.Length);
            bw.Write(Header);
            bw.Write(Data);
            bw.WriteUInt16BE(Magic2);

            byte[] packet = ms.ToArray();

            return packet;
        }
    }
}
