using System.CommandLine;
using Weedwacker.GameServer.Data;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel;
using Weedwacker.GameServer.Data.Excel.Common;
using Weedwacker.Shared.Commands;
using Weedwacker.Shared.Commands.Definitions;
using Weedwacker.Shared.Enums;

namespace Weedwacker.GameServer.Commands.Definitions
{
    internal class ListCommandDefinitions : ICommandDefinition
    {
        private readonly Command _command;

        public ListCommandDefinitions()
        {
            _command = CreateCommand();
        }

        public IEnumerable<Command> GetCommands()
        {
            yield return _command;
        }

        private Command CreateCommand()
        {
            RankedCommand list = new(UserRank.Player, "list", "Commands to list various objects.");

            RankedCommand players = new(UserRank.Player, "players", "List all players in the same session.");
            players.SetHandler(context => ListPlayers(context.Console));

            RankedCommand avatars = new(UserRank.Player, "avatars", "List all avatars available.");
            avatars.SetHandler(context => ListAvatars(context.Console));

            RankedCommand avatarProps = new(UserRank.Player, "avatar_props", "List all properties applicable to avatars.");
            avatarProps.SetHandler(context => ListAvatarProps(context.Console));

            RankedCommand items = new(UserRank.Player, "items", "List available items of a certain category.");

            RankedCommand allItems = new(UserRank.Player, "all", "Lists all items available.");
            allItems.SetHandler(context => ListItems(x => x.itemType is ItemType.ITEM_MATERIAL or ItemType.ITEM_WEAPON or ItemType.ITEM_RELIQUARY or ItemType.ITEM_FURNITURE, context.Console));
            RankedCommand materials = new(UserRank.Player, "materials", "Lists all materials available.");
            materials.SetHandler(context => ListItems(x => x.itemType is ItemType.ITEM_MATERIAL, context.Console));
            RankedCommand weapons = new(UserRank.Player, "weapons", "Lists all weapons available.");
            weapons.SetHandler(context => ListItems(x => x.itemType is ItemType.ITEM_WEAPON, context.Console));
            RankedCommand artifacts = new(UserRank.Player, "artifacts", "Lists all artifacts available.");
            artifacts.SetHandler(context => ListItems(x => x.itemType is ItemType.ITEM_RELIQUARY, context.Console));
            RankedCommand furniture = new(UserRank.Player, "furnitures", "Lists all furniture available.");
            furniture.SetHandler(context => ListItems(x => x.itemType is ItemType.ITEM_FURNITURE, context.Console));

            items.AddCommand(allItems);
            items.AddCommand(materials);
            items.AddCommand(weapons);
            items.AddCommand(artifacts);
            items.AddCommand(furniture);

            RankedCommand monsters = new(UserRank.Player, "monsters", "List available monsters of a certain category.");

            RankedCommand allMonsters = new(UserRank.Player, "all", "Lists all monsters available.");
            allMonsters.SetHandler(context => ListMonsters(x => x.type is MonsterType.MONSTER_ORDINARY or MonsterType.MONSTER_BOSS or MonsterType.MONSTER_ENV_ANIMAL, context.Console));
            RankedCommand normal = new(UserRank.Player, "normals", "Lists all normal monsters available.");
            normal.SetHandler(context => ListMonsters(x => x.type is MonsterType.MONSTER_ORDINARY, context.Console));
            RankedCommand bosses = new(UserRank.Player, "bosses", "Lists all boss monsters available.");
            bosses.SetHandler(context => ListMonsters(x => x.type is MonsterType.MONSTER_BOSS, context.Console));
            RankedCommand animals = new(UserRank.Player, "animals", "Lists all animals available.");
            animals.SetHandler(context => ListMonsters(x => x.type is MonsterType.MONSTER_ENV_ANIMAL, context.Console));

            monsters.AddCommand(allMonsters);
            monsters.AddCommand(normal);
            monsters.AddCommand(bosses);
            monsters.AddCommand(animals);

            list.AddCommand(players);
            list.AddCommand(avatars);
            list.AddCommand(avatarProps);
            list.AddCommand(items);
            list.AddCommand(monsters);

            return list;
        }

        private void ListPlayers(IConsole output)
        {
            IList<(string, string)> playerInfo = GameServer.OnlinePlayers.Values
                .OrderBy(x => x.Player?.GameUid ?? 0)
                .Select(x => ($"{x.Player?.GameUid ?? 0}", x.Player?.NickName ?? string.Empty))
                .ToList();

            if (playerInfo.Count <= 0)
            {
                output.WriteLine("No players online.");
                return;
            }

            int maxIdSize = playerInfo.Max(x => x.Item1.Length) + 2;
            foreach ((string, string) element in playerInfo)
                output.WriteLine($"{element.Item1}{new string(' ', maxIdSize - element.Item1.Length)}{element.Item2}");
        }

        private void ListAvatars(IConsole output)
        {
            IList<(string, string)> avatarInfo = GameData.AvatarDataMap.Values
                .OrderBy(x => x.id)
                .Select(x => ($"{x.id}", AvatarCommandDefinition.AvatarLookup.GetValueOrDefault(x.id, string.Empty)))
                .ToList();

            if (avatarInfo.Count <= 0)
            {
                output.WriteLine("No avatars available.");
                return;
            }

            int maxIdSize = avatarInfo.Max(x => x.Item1.Length) + 2;
            foreach ((string, string) element in avatarInfo)
                output.WriteLine($"{element.Item1}{new string(' ', maxIdSize - element.Item1.Length)}{element.Item2}");
        }

        private void ListAvatarProps(IConsole output)
        {
            IList<(string, string)> propInfo = Enum.GetValues<FightPropType>()
                .OrderBy(x => x)
                .Select(x => ($"{(int)x}", x.ToString()))
                .ToList();

            if (propInfo.Count <= 0)
            {
                output.WriteLine("No avatar properties available.");
                return;
            }

            int maxIdSize = propInfo.Max(x => x.Item1.Length) + 2;
            foreach ((string, string) element in propInfo)
                output.WriteLine($"{element.Item1}{new string(' ', maxIdSize - element.Item1.Length)}{element.Item2}");
        }

        private void ListItems(Func<ItemConfig, bool> filter, IConsole output)
        {
            IList<(string, string, string)> items = GameData.ItemDataMap.Values
                .Where(filter)
                .OrderBy(x => x.itemType).ThenBy(x => x.id)
                .Select(x => ($"{x.id}", x.itemType.ToString(), GiveCommandDefinition.ItemLookup.GetValueOrDefault(x.id, string.Empty)))
                .ToList();

            if (items.Count <= 0)
            {
                output.WriteLine("No items available.");
                return;
            }

            int maxIdSize = items.Max(x => x.Item1.Length) + 2;
            int maxTypeSize = items.Max(x => x.Item2.Length) + 2;
            foreach ((string, string, string) element in items)
                output.WriteLine($"{element.Item1}{new string(' ', maxIdSize - element.Item1.Length)}{element.Item2}{new string(' ', maxTypeSize - element.Item2.Length)}{element.Item3}");
        }

        private void ListMonsters(Func<MonsterExcelConfig, bool> filter, IConsole output)
        {
            IList<(string, string, string)> monsterInfo = GameData.MonsterDataMap.Values
                .Where(filter)
                .OrderBy(x => x.type).ThenBy(x => x.id)
                .Select(x => ($"{x.id}", x.type.ToString(), SpawnCommandDefinition.MonsterLookup.GetValueOrDefault(x.id, string.Empty)))
                .ToList();

            if (monsterInfo.Count <= 0)
            {
                output.WriteLine("No monsters available.");
                return;
            }

            int maxIdSize = monsterInfo.Max(x => x.Item1.Length) + 2;
            int maxTypeSize = monsterInfo.Max(x => x.Item2.Length) + 2;
            foreach ((string, string, string ) element in monsterInfo)
                output.WriteLine($"{element.Item1}{new string(' ', maxIdSize - element.Item1.Length)}{element.Item2}{new string(' ', maxTypeSize - element.Item2.Length)}{element.Item3}");
        }
    }
}
