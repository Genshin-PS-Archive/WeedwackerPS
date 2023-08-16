using Weedwacker.GameServer.Commands.Receivers;
using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer;

internal partial class Connection
{
    [OpCode((ushort)OpCode.PrivateChatReq)]
    private async Task HandlePrivateChatReq(byte[] header, byte[] payload)
    {
        PrivateChatReq req = PrivateChatReq.Parser.ParseFrom(payload);

        // Create chat packet.
        PacketPrivateChatNotify notify = new(Player.GameUid, req.TargetUid, req.Text);

        // Send and put in history.
        await SendPacketAsync(notify);

        //handle commands
        if (req.TargetUid == GameServer.Configuration.Server.GameOptions.Constants.SERVER_CONSOLE_UID)
        {
            if (req.Text[0] == '/')
            {
                try
                {
                    string result = PrivateChatReceiver.Instance.OnPrivateChatCommandReceived(req.Text[1..]);

                    await SendPacketAsync(new PacketPrivateChatNotify(req.TargetUid, Player.GameUid, result));
                }
                catch (Exception e)
                {
                    Logger.WriteErrorLine(e.ToString());
                }
            }
            await SendPacketAsync(new PacketPrivateChatRsp());
        }
    }
}
