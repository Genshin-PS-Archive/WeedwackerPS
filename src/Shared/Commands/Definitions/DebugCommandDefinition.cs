using System.CommandLine;
using Weedwacker.Shared.Enums;

namespace Weedwacker.Shared.Commands.Definitions
{
    public class DebugCommandDefinition : ICommandDefinition
    {
        private readonly Command _command;

        public DebugCommandDefinition()
        {
            _command = CreateCommands();
        }

        public IEnumerable<Command> GetCommands()
        {
            yield return _command;
        }

        protected virtual Command CreateCommands()
        {
            RankedCommand root = new(UserRank.Dev, "debug", "Commands to debug the current state of the game session.");

            Command hash = CreateHashCommand();

            root.AddCommand(hash);

            return root;
        }

        protected virtual Command CreateHashCommand()
        {
            RankedCommand hash = new(UserRank.Dev, "hash", "Commands to work on and with hashes.");

            RankedCommand ability = new(UserRank.Dev, "ability", "Hash a given ability name.");
            Argument<string> nameArg = new("name", "The ability name to hash.");
            ability.Add(nameArg);
            ability.SetHandler(context => HashAbility(context.ParseResult.GetValueForArgument(nameArg), context.Console));

            RankedCommand path = new(UserRank.Dev, "path", "Hash a given path.");
            Argument<string> pathNameArg = new("name", "The path name to hash.");
            path.Add(pathNameArg);
            path.SetHandler(context => HashPath(context.ParseResult.GetValueForArgument(pathNameArg), context.Console));

            hash.AddCommand(ability);
            hash.AddCommand(path);

            return hash;
        }

        private void HashAbility(string name, IConsole output)
        {
            output.WriteLine($"{Utils.Utils.AbilityHash(name)}");
        }

        private void HashPath(string name, IConsole output)
        {
            output.WriteLine($"0x{Utils.Utils.GetPathHash(name):X}");
        }
    }
}
