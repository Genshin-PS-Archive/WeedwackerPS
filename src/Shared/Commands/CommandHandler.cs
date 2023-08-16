using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.CommandLine.Parsing;
using Weedwacker.Shared.Commands.Definitions;
using Weedwacker.Shared.Commands.Receivers;
using Weedwacker.Shared.Enums;

namespace Weedwacker.Shared.Commands
{
    public class CommandHandler : ICommandHandler
    {
        private static readonly Lazy<CommandHandler> Lazy = new(() => new CommandHandler());
        public static CommandHandler Default => Lazy.Value;

        private readonly IEnumerable<ICommandReceiver> _receivers;
        private readonly Parser _commandLineParser;

        private CommandHandler()
        {
            CommandLineBuilder builder = new CommandLineBuilder().UseDefaults().AddMiddleware(AddRankMiddleware);

            _receivers = new[] { CommandLineReceiver.Default };
            _commandLineParser = builder.Build();
        }

        public CommandHandler(IEnumerable<ICommandReceiver> receivers, IEnumerable<ICommandDefinition> commands)
        {
            CommandLineBuilder builder = new CommandLineBuilder().UseDefaults().AddMiddleware(AddRankMiddleware);

            _receivers = receivers;
            _commandLineParser = builder.Build();

            foreach (Command command in commands.SelectMany(x => x.GetCommands()))
                builder.Command.AddCommand(command);
        }

        public void Start()
        {
            foreach (ICommandReceiver receiver in _receivers)
            {
                receiver.CommandReceived += Receiver_CommandReceived;
                receiver.Start();
            }
        }

        public void Invoke(string command, IConsole output, UserRank inputRank)
        {
            _commandLineParser.InvokeAsync(command, new RankedConsoleStub(output, inputRank));
        }

        private void Receiver_CommandReceived(object? sender, CommandReceivedEventArgs e)
        {
            Invoke(e.CommandInput, e.Output, e.Rank);
        }

        private async Task AddRankMiddleware(InvocationContext context, Func<InvocationContext, Task> next)
        {
            if (context.ParseResult.CommandResult.Command is RankedCommand rankedCommand &&
                context.Console is RankedConsoleStub rankedInput)
            {
                // If the command can be executed by the receiver
                if (rankedInput.Rank >= rankedCommand.Rank)
                {
                    await next(context);
                    return;
                }

                // Otherwise, output a message that the command is not allowed
                context.Console.WriteLine("You're not allowed to use that command!");
                return;
            }

            // If one member cannot provide a rank, continue normally
            await next(context);
        }

        readonly struct RankedConsoleStub : IConsole
        {
            private readonly IConsole _forward;

            public IStandardStreamWriter Out => _forward.Out;
            public bool IsOutputRedirected => _forward.IsOutputRedirected;
            public IStandardStreamWriter Error => _forward.Error;
            public bool IsErrorRedirected => _forward.IsErrorRedirected;
            public bool IsInputRedirected => _forward.IsInputRedirected;

            public UserRank Rank { get; }

            public RankedConsoleStub(IConsole forward, UserRank rank)
            {
                _forward = forward;
                Rank = rank;
            }
        }
    }
}
