using System.CommandLine;
using Weedwacker.Shared.Enums;

namespace Weedwacker.Shared.Commands.Receivers
{
    public class CommandReceivedEventArgs : EventArgs
    {
        public string CommandInput { get; }
        public IConsole Output { get; }
        public UserRank Rank { get; }

        public CommandReceivedEventArgs(string input, IConsole output, UserRank rank)
        {
            CommandInput = input;
            Output = output;
            Rank = rank;
        }
    }
}
