﻿using System.Net;
using System.Net.Sockets;
using KcpSharp;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer
{
    internal class Listener
    {
        public const int MAX_MSG_SIZE = 16384;
        public const int HANDSHAKE_SIZE = 20;
        private static Socket? UDPListener => UDPClient?.Client;
        private static UdpClient? UDPClient;
        private static IPEndPoint? ListenAddress;
        private static IKcpTransport<IKcpMultiplexConnection>? KCPTransport;
        private static readonly CancellationTokenSource CancelToken = new();
        private static IKcpMultiplexConnection? Multiplex => KCPTransport?.Connection;
        public static readonly SortedList<long, Connection> Connections = new();
        public static Connection? GetConnectionByEndPoint(IPEndPoint ep) => Connections.Values.FirstOrDefault(c => c.RemoteEndPoint.Equals(ep));

        private static readonly KcpConversationOptions ConvOpt = new()
        {
            StreamMode = false,
            Mtu = 1400,
            ReceiveWindow = 256,
            SendWindow = 256,
            NoDelay = true,
            UpdateInterval = 100,
            KeepAliveOptions = new KcpKeepAliveOptions(1000, 30000)
        };
        private static uint PORT => GameServer.Configuration.Server.AccessPort;
        public static void StartListener()
        {
            ListenAddress = new IPEndPoint(IPAddress.Parse(GameServer.Configuration.Server.AccessAddress), (int)PORT);
            UDPClient = new UdpClient(ListenAddress);
            if (UDPListener == null) return;
            KCPTransport = KcpSocketTransport.CreateMultiplexConnection(UDPClient, 1400);
            KCPTransport.Start();
            Logger.WriteLine($"Game Server started. Listening on port: {PORT}");
        }

        private static void RegisterConnection(Connection con)
        {
            if (!con.ConversationID.HasValue) return;
            Connections[con.ConversationID.Value] = con;
        }
        public static void UnregisterConnection(Connection con)
        {
            if (!con.ConversationID.HasValue) return;
            long convId = con.ConversationID.Value;
            if (Connections.Remove(convId))
            {
                Multiplex?.UnregisterConversation(convId);
                Logger.WriteLine($"Connection with {con.RemoteEndPoint} has been closed");
            }
        }
        public static async Task HandleHandshake(UdpReceiveResult rcv)
        {
            try
            {
                Connection? con = GetConnectionByEndPoint(rcv.RemoteEndPoint);
                await using MemoryStream? ms = new(rcv.Buffer);
                using BinaryReader? br = new(ms);
                int code = br.ReadInt32BE();
                br.ReadUInt32();
                br.ReadUInt32();
                int enet = br.ReadInt32BE();
                br.ReadUInt32();
                switch (code)
                {
                    case 0x000000FF:
                        if (con != null)
                        {
                            Logger.WriteLine($"Duplicate handshake from {con.RemoteEndPoint}");
                            return;
                        }
                        await AcceptConnection(rcv, enet);
                        break;
                    case 0x00000194:
                        if (con == null)
                        {
                            Logger.WriteLine($"Inexistent connection asked for disconnect from {rcv.RemoteEndPoint}");
                            return;
                        }
                        await SendDisconnectPacket(con, 5);
                        break;
                    default:
                        Logger.WriteErrorLine($"Invalid handshake code received {code}");
                        return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLine($"Failed to handle handshake: {ex}");
            }
        }

        private static async Task AcceptConnection(UdpReceiveResult rcv, int enet)
        {
            long convId = Connections.GetNextAvailableIndex();
            KcpConversation? convo = Multiplex?.CreateConversation(convId, rcv.RemoteEndPoint, ConvOpt);
            if (convo == null) return;
            Connection? con = new(convo, rcv.RemoteEndPoint);
            RegisterConnection(con);
            await SendHandshakeResponse(con, enet);
        }

        private static async Task SendHandshakeResponse(Connection user, int enet)
        {
            if (user == null || UDPClient == null || !user.ConversationID.HasValue) return;
            long convId = user.ConversationID.Value;
            await using MemoryStream? ms = new();
            using BinaryWriter? bw = new(ms);
            bw.WriteInt32BE(0x00000145);
            bw.WriteConvID(convId);
            bw.WriteInt32BE(enet);
            bw.WriteInt32BE(0x14514545);
            byte[]? data = ms.ToArray();
            await UDPClient.SendAsync(data, data.Length, user.RemoteEndPoint);
        }
        public static async Task SendDisconnectPacket(Connection user, int code)
        {
            if (user == null || UDPClient == null || !user.ConversationID.HasValue) return;
            long convId = user.ConversationID.Value;
            await using MemoryStream? ms = new();
            using BinaryWriter? bw = new(ms);
            bw.WriteInt32BE(0x00000194);
            bw.WriteConvID(convId);
            bw.WriteInt32BE(code);
            bw.WriteInt32BE(0x19419494);
            byte[]? data = ms.ToArray();
            await UDPClient.SendAsync(data, data.Length, user.RemoteEndPoint);
        }
    }
}
