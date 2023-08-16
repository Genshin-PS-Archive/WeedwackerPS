using System.CommandLine;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Systems.Avatar;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Commands;
using Weedwacker.Shared.Commands.Definitions;
using Weedwacker.Shared.Enums;

namespace Weedwacker.GameServer.Commands.Definitions
{
    internal class AvatarCommandDefinition : ICommandDefinition
    {
        public static readonly Dictionary<uint, string> AvatarLookup = new()
        {
            [10000001] = "Kate",
            [10000002] = "Kamisato Ayaka",
            [10000003] = "Jean",
            [10000005] = "Traveler",
            [10000006] = "Lisa",
            [10000007] = "Traveler",
            [10000014] = "Barbara",
            [10000015] = "Kaeya",
            [10000016] = "Diluc",
            [10000020] = "Razor",
            [10000021] = "Amber",
            [10000022] = "Venti",
            [10000023] = "Xiangling",
            [10000024] = "Beidou",
            [10000025] = "Xingqiu",
            [10000026] = "Xiao",
            [10000027] = "Ningguang",
            [10000029] = "Klee",
            [10000030] = "Zhongli",
            [10000031] = "Fischl",
            [10000032] = "Bennett",
            [10000033] = "Tartaglia",
            [10000034] = "Noelle",
            [10000035] = "Qiqi",
            [10000036] = "Chongyun",
            [10000037] = "Ganyu",
            [10000038] = "Albedo",
            [10000039] = "Diona",
            [10000041] = "Mona",
            [10000042] = "Keqing",
            [10000043] = "Sucrose",
            [10000044] = "Xinyan",
            [10000045] = "Rosaria",
            [10000046] = "Hu Tao",
            [10000047] = "Kaedehara Kazuha",
            [10000048] = "Yanfei",
            [10000049] = "Yoimiya",
            [10000050] = "Thoma",
            [10000051] = "Eula",
            [10000052] = "Raiden Shogun",
            [10000053] = "Sayu",
            [10000054] = "Sangonomiya Kokomi",
            [10000055] = "Gorou",
            [10000056] = "Kujou Sara",
            [10000057] = "Arataki Itto",
            [10000058] = "Yae Miko",
            [10000059] = "Shikanoin Heizou",
            [10000060] = "Yelan",
            [10000062] = "Aloy",
            [10000063] = "Shenhe",
            [10000064] = "Yun Jin",
            [10000065] = "Kuki Shinobu",
            [10000066] = "Kamisato Ayato",
            [10000067] = "Collei",
            [10000068] = "Dori",
            [10000069] = "Tighnari",
            [10000070] = "Nilou",
            [10000071] = "Cyno",
            [10000072] = "Candace",
            [10000073] = "Nahida",
            [10000074] = "Layla",
            [11000008] = "Party Test #4",
            [11000009] = "Background Test",
            [11000010] = "Naked Model #1",
            [11000011] = "Naked Man",
            [11000013] = "Co-Op Test",
            [11000017] = "Adult Male Body Test",
            [11000018] = "Adult Female Body Test",
            [11000019] = "Girl Body Test",
            [11000025] = "Akuliya",
            [11000026] = "Yaoyao (Trial)",
            [11000027] = "Girl Body Test - #2 Machine",
            [11000028] = "Shiro Maiden",
            [11000030] = "Greatsword Maiden",
            [11000031] = "Late Weapon Test A",
            [11000032] = "Late Weapon Test B",
            [11000033] = "Late Weapon Test C",
            [11000034] = "Late Weapon Test D",
            [11000035] = "Lance Warrioress",
            [11000036] = "Swordswoman Test",
            [11000037] = "Rx White-Box",
            [11000038] = "Boy Body Test",
            [11000039] = "Adult Male Body Test",
            [11000040] = "Female Lead New Normal Attack",
            [11000041] = "Male Lead New Normal Attack",
            [11000042] = "Chongyun (Test)",
            [11000043] = "Test Character",
            [11000044] = "Qiqi (Test)",
            [11000045] = "Diona (Test)",
        };

        private readonly Command _command;

        public AvatarCommandDefinition()
        {
            _command = CreateCommand();
        }

        public IEnumerable<Command> GetCommands()
        {
            yield return _command;
        }

        private Command CreateCommand()
        {
            RankedCommand avatar = new(UserRank.Player, "avatar", "Commands to act on avatars.");

            // Add current command
            RankedCommand current = new(UserRank.Player, "current", "Shows information on the current avatar of the player.");
            Argument<uint> currentUidArg = new("uid", "The UID of the player to show the current avatar from.");
            current.AddArgument(currentUidArg);
            current.SetHandler(context => PrintCurrentAvatar(context.ParseResult.GetValueForArgument(currentUidArg), context.Console));

            // Add get command
            RankedCommand get = new(UserRank.Player, "get", "Shows a specific property on the avatar of the player.");
            Argument<uint> getUidArg = new("uid", "The UID of the player to use the avatars from.");
            Argument<uint> getIdArg = new("id", "The ID of the avatar to get properties from. Use 'list avatars' for all avatar ID's.");
            Argument<string> getPropArg = new("prop", "The ID of the property to get or 'all'. Use 'list avatar_props' for all prop ID's.");
            get.AddArgument(getUidArg);
            get.AddArgument(getIdArg);
            get.AddArgument(getPropArg);
            get.SetHandler(context => PrintAvatarFightProp(
                context.ParseResult.GetValueForArgument(getUidArg),
                context.ParseResult.GetValueForArgument(getIdArg),
                context.ParseResult.GetValueForArgument(getPropArg),
                context.Console));

            // Add set command
            RankedCommand set = new(UserRank.GM, "set", "Sets a specific property on the avatar of the player.");
            Argument<uint> setUidArg = new("uid", "The UID of the player to use the avatars from.");
            Argument<uint> setIdArg = new("id", "The ID of the avatar to set a property on. Use 'list avatars' for all avatar ID's.");
            Argument<string> setPropArg = new("prop", "The ID of the property to set. Use 'list avatar_props' for all prop ID's.");
            Argument<float> setValueArg = new("value", "The value to set the property to.");
            set.AddArgument(setUidArg);
            set.AddArgument(setIdArg);
            set.AddArgument(setPropArg);
            set.AddArgument(setValueArg);
            set.SetHandler(context => SetAvatarFightProp(
                context.ParseResult.GetValueForArgument(setUidArg),
                context.ParseResult.GetValueForArgument(setIdArg),
                context.ParseResult.GetValueForArgument(setPropArg),
                context.ParseResult.GetValueForArgument(setValueArg),
                context.Console));

            avatar.AddCommand(current);
            avatar.AddCommand(get);
            avatar.AddCommand(set);

            return avatar;
        }

        private void PrintCurrentAvatar(uint uid, IConsole output)
        {
            if (!GameServer.OnlinePlayers.TryGetValue(uid, out Connection? playerConn))
            {
                output.WriteLine("Player isn't online or doesn't exist.");
                return;
            }

            AvatarEntity? current = playerConn.Player?.TeamManager.CurrentAvatarEntity;
            if (current == null)
            {
                output.WriteLine("Player has no avatar selected.");
                return;
            }

            // Print avatar info
            if (!AvatarLookup.TryGetValue(current.Avatar.AvatarId, out string? name))
                name = string.Empty;

            output.WriteLine($"{name} ({current.Avatar.AvatarId})");
            foreach (FightPropType prop in Enum.GetValues<FightPropType>())
            {
                if (current.FightProps.TryGetValue(prop, out float propValue))
                    output.WriteLine($"{prop}: {propValue}");
            }
        }

        private void PrintAvatarFightProp(uint uid, uint id, string propId, IConsole output)
        {
            if (!GameServer.OnlinePlayers.TryGetValue(uid, out Connection? playerConn))
            {
                output.WriteLine("Player isn't online or doesn't exist.");
                return;
            }

            Avatar? avatar = playerConn.Player?.Avatars?.GetAvatarById(id);
            if (avatar == null)
            {
                output.WriteLine("Player does not own this avatar.");
                return;
            }

            float propValue;
            if (propId == "all")
            {
                foreach (FightPropType prop in Enum.GetValues<FightPropType>())
                {
                    if (avatar.FightProp.TryGetValue(prop, out propValue))
                        output.WriteLine($"{prop}: {propValue}");
                }

                return;
            }

            FightPropType parsedProp;
            if (int.TryParse(propId, out int intPropId))
                parsedProp = (FightPropType)intPropId;
            else if (!Enum.TryParse(propId, true, out parsedProp))
            {
                output.WriteLine("PropId is unknown.");
                return;
            }

            if (avatar.FightProp.TryGetValue(parsedProp, out propValue))
                output.WriteLine($"{parsedProp}: {propValue}");
        }

        private void SetAvatarFightProp(uint uid, uint id, string propId, float value, IConsole output)
        {
            if (!GameServer.OnlinePlayers.TryGetValue(uid, out Connection? playerConn))
            {
                output.WriteLine("Player isn't online or doesn't exist.");
                return;
            }

            Avatar? avatar = playerConn.Player?.Avatars?.GetAvatarById(id);
            if (avatar == null)
            {
                output.WriteLine("Player does not own this avatar.");
                return;
            }

            FightPropType parsedProp;
            if (int.TryParse(propId, out int intPropId))
                parsedProp = (FightPropType)intPropId;
            else if (!Enum.TryParse(propId, true, out parsedProp))
            {
                output.WriteLine("PropId is unknown.");
                return;
            }

            AvatarEntity? entity = playerConn.Player?.TeamManager.ActiveTeam.Values.FirstOrDefault(x => x.Avatar.AvatarId == id);
            if (entity != null)
                entity.SetFightProperty(parsedProp, value).Wait();
            else
                avatar.SetFightProperty(parsedProp, value).Wait();
        }
    }
}
