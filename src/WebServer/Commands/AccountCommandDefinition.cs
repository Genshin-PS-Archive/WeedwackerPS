using System.CommandLine;
using Weedwacker.Shared.Commands;
using Weedwacker.Shared.Commands.Definitions;
using Weedwacker.Shared.Enums;
using Weedwacker.WebServer.Database;

namespace Weedwacker.WebServer.Commands
{
    class AccountCommandDefinition : ICommandDefinition
    {
        private readonly Command _command;

        public AccountCommandDefinition()
        {
            _command = CreateCommand();
        }

        public IEnumerable<Command> GetCommands()
        {
            yield return _command;
        }

        private Command CreateCommand()
        {
            RankedCommand accountCommand = new(UserRank.Console, "account", "Commands related to account operations.");

            RankedCommand createCommand = new(UserRank.Console, "create", "Create an account");
            Argument<string> nameArgument = new("username", "The username for the account.");
            Option<string> passwordOption = new("-p", "The password for the account.");
            Option<string> uidOption = new("-u", () => "0", "The UID for the account.");
            createCommand.AddAlias("add");
            createCommand.AddArgument(nameArgument);
            createCommand.AddOption(uidOption);
            createCommand.AddOption(passwordOption);
            createCommand.SetHandler(context => CreateAccount(
                context.ParseResult.GetValueForArgument(nameArgument),
                context.ParseResult.GetValueForOption(passwordOption),
                context.ParseResult.GetValueForOption(uidOption)!,
                context.Console));

            accountCommand.AddCommand(createCommand);

            return accountCommand;
        }

        private void CreateAccount(string name, string? password, string uid, IConsole output)
        {
            if (string.IsNullOrEmpty(name))
            {
                output.WriteLine("Username is invalid.");
                return;
            }

            Account? account = DatabaseManager.GetAccountByNameAsync(name).Result;
            if (account != null)
            {
                output.WriteLine($"Username {name} already exists.");
                return;
            }

            DatabaseManager.CreateAccountWithUid(name, password, uid);

            output.WriteLine($"Successfully created account {name}.");
        }
    }
}
