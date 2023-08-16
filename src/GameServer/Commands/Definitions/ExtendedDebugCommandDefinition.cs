using System.CommandLine;
using Newtonsoft.Json;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.Shared.Commands;
using Weedwacker.Shared.Commands.Definitions;
using Weedwacker.Shared.Enums;
using Weedwacker.Shared.Utils;
using Weedwacker.Shared.Utils.Configuration;

namespace Weedwacker.GameServer.Commands.Definitions
{
    internal class ExtendedDebugCommandDefinition : DebugCommandDefinition
    {
        protected override Command CreateCommands()
        {
            Command root = base.CreateCommands();

            // Config commands
            RankedCommand config = new(UserRank.Dev, "config", "Commands to work with the config of the game server.");

            RankedCommand reload = new(UserRank.Dev, "reload", "Reloads the config of the game server.");
            reload.SetHandler(context => ReloadConfig(context.Console));

            config.AddCommand(reload);

            // State commands
            RankedCommand state = new(UserRank.Dev, "state", "Sets the given player's current menu state.");
            Argument<uint> uidArg = new("uid", "The UID of the player to set the state on.");
            Argument<string> stateArg = new("id", "The ID of the state to set. Use 'list states' for all state ID's.");

            state.AddArgument(uidArg);
            state.AddArgument(stateArg);
            state.SetHandler(context => SetOpenState(
                context.ParseResult.GetValueForArgument(uidArg),
                context.ParseResult.GetValueForArgument(stateArg),
                context.Console));

            root.AddCommand(config);
            root.AddCommand(state);

            return root;
        }

        protected override Command CreateHashCommand()
        {
            Command hash = base.CreateHashCommand();

            // Extend hash commands
            RankedCommand export = new(UserRank.Dev, "export", "Export all ability hashes to a file.");
            export.SetHandler(context => ExportHashes(context.Console));

            hash.AddCommand(export);

            return hash;
        }

        private void ExportHashes(IConsole output)
        {
            string jsonString = JsonConvert.SerializeObject(GameServer.AbilityNameHashMap, Formatting.Indented);
            File.WriteAllText("ability_hashMap.json", jsonString);

            output.WriteLine("Exported ability hashes.");
        }

        private void ReloadConfig(IConsole output)
        {
            try
            {
                GameConfig config = Config.Load<GameConfig>("GameConfig.json").Result;
                GameServer.Configuration = config;

                output.WriteLine("Reloaded GameConfig.");
            }
            catch (Exception e)
            {
                Logger.WriteErrorLine("An error occurred while reloading GameConfig.json.", e);
            }
        }

        private void SetOpenState(uint uid, string stateId, IConsole output)
        {
            if (!GameServer.OnlinePlayers.TryGetValue(uid, out Connection? playerConn))
            {
                output.WriteLine("Player isn't online or doesn't exist.");
                return;
            }

            OpenStateType parsedState;
            if (int.TryParse(stateId, out int intStateId))
                parsedState = (OpenStateType)intStateId;
            else if (!Enum.TryParse(stateId, true, out parsedState))
            {
                output.WriteLine("Invalid state ID.");
                return;
            }

            playerConn.Player?.OpenStateManager.SetOpenStateAsync((uint)parsedState, 1).Wait();
            output.WriteLine($"Set state {stateId} for player {uid}.");
        }
    }
}
