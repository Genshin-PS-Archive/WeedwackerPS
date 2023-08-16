using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Extensions.Options;
using Weedwacker.Shared.Commands.Configuration;
using Weedwacker.Shared.Enums;

namespace Weedwacker.Shared.Commands.Receivers;

public class TcpReceiver : ICommandReceiver
{
    public event EventHandler<CommandReceivedEventArgs>? CommandReceived;

    private const UserRank Rank_ = UserRank.Console;

    private readonly CancellationTokenSource _cts;
    private readonly IPEndPoint              _endpoint;
    private readonly Socket                  _server;
    private readonly Thread                  _thread;

    public TcpReceiver(IOptions<TcpReceiverConfig> options)
    {
        TcpReceiverConfig config = options.Value;

        _cts      = new CancellationTokenSource();
        _endpoint = new IPEndPoint(IPAddress.Parse(config.ListenAddress), config.ListenPort);
        _server   = new Socket(_endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        _thread   = new Thread(MainLoop)
        {
            IsBackground = true,
            Name = "TCP command receiving thread"
        };
    }

    private void MainLoop()
    {
        Span<byte>    buffer = stackalloc byte[1024]; // surely we won't need more than 1KiB right?
        StaticConsole output = new();

        while (!_cts.IsCancellationRequested)
        {
            using Socket socket = _server.Accept();

            while (!_cts.IsCancellationRequested)
            {
                int read = socket.Receive(buffer, SocketFlags.None, out SocketError errorCode);
                if (errorCode != SocketError.Success)
                    break;

                string cmd = Encoding.UTF8.GetString(
                    buffer.Slice(0, read));

                CommandReceived?.Invoke(
                    this,
                    new CommandReceivedEventArgs(cmd, output, Rank_));

                Send(socket, output.GetResult());
            }
        }
    }

    // Factored out due to CA2014
    private static void Send(Socket socket, ReadOnlySpan<char> chars)
    {
        Span<byte> buffer = stackalloc byte[chars.Length];
        Encoding.UTF8.GetBytes(chars, buffer);

        socket.Send(buffer, SocketFlags.None, out _);
    }

    public void Start()
    {
        _server.Bind(_endpoint);
        _server.Listen();
        _thread.Start();
    }

    public void Dispose()
    {
        _cts.Cancel();
        _server.Dispose();
        _thread.Join();
    }
}