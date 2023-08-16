using System.CommandLine;
using Weedwacker.Shared.Commands.Receivers;
using Weedwacker.Shared.Enums;

namespace Weedwacker.Shared.Commands
{
    public interface ICommandHandler
    {
        /// <summary>
        /// Start receiving commands from <see cref="ICommandReceiver"/>'s.
        /// </summary>
        void Start();

        /// <summary>
        /// Manually input a command to execute.
        /// </summary>
        /// <param name="command">The command to invoke.</param>
        /// <param name="output">The output to write the result to.</param>
        /// <param name="rank">The rank the user currently has.</param>
        void Invoke(string command, IConsole output, UserRank rank);
    }
}
