using System.CommandLine;

namespace Weedwacker.Shared.Commands.Definitions
{
    public interface ICommandDefinition
    {
        public IEnumerable<Command> GetCommands();
    }
}
