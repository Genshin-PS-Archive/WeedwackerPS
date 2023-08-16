namespace Weedwacker.Shared.Commands.Configuration;

public class TcpReceiverConfig
{
    public const string ConfigKey = "Server:CommandReceivers:TcpReceiver";

    public required string ListenAddress { get; init; }
    public required ushort ListenPort    { get; init; }
}