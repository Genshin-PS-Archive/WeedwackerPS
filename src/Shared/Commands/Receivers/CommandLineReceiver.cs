using System.CommandLine;
using System.CommandLine.IO;
using Weedwacker.Shared.Enums;

namespace Weedwacker.Shared.Commands.Receivers
{
    public class CommandLineReceiver : ICommandReceiver
    {
        private static readonly Lazy<CommandLineReceiver> Lazy = new(() => new CommandLineReceiver());
        public static CommandLineReceiver Default => Lazy.Value;

        private readonly IConsole _console = new SystemConsole();
        private readonly CancellationTokenSource _token = new();
        private readonly UserRank _rank = UserRank.Console;

        public event EventHandler<CommandReceivedEventArgs>? CommandReceived;

        private CommandLineReceiver(UserRank rank)
        {
            _rank = rank;
        }

        public CommandLineReceiver() { }

        public static ICommandReceiver Create(UserRank rank) => new CommandLineReceiver(rank);

        public void Start()
        {
            Task.Run(ReceiveLoop);
        }

        private void ReceiveLoop()
        {
            while (!_token.IsCancellationRequested)
            {
                Console.Write("> ");

                string? args = Console.ReadLine();
                if (args == null)
                    continue;

                OnCommandReceived(args);
            }
        }

        private void OnCommandReceived(string args)
        {
            CommandReceived?.Invoke(this, new CommandReceivedEventArgs(args, _console, _rank));
        }

        public void Dispose()
        {
            _token.Cancel();
        }
    }
}
