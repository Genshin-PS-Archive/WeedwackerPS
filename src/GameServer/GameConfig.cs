using Newtonsoft.Json;
using Weedwacker.GameServer.Enums;
using Weedwacker.Shared.Network.Proto;
using Weedwacker.Shared.Utils.Configuration;

namespace Weedwacker.GameServer
{
    internal class GameConfig : ConfigFile
    {
        public ServerJson Server = new();
        public DatabaseJson Database = new();
        public new StructureJson structure = new();

        public new class DatabaseJson
        {
            public string ConnectionUri = "mongodb://localhost:27017";
            public string Database = "weedwackerGame";
        }

        public new class StructureJson
        {
            public string Resources = Path.Combine("..","..","..","resources");
            public string Cache = Path.Combine("..", "..", "..", "cache");
            public string Scripts = Path.Combine("..", "..", "..", "resources","lua");
            public string keys = Path.Combine("..", "..", "..","keys");
        }

        public class ServerJson
        {
            /* This is the address used in the default region. */
            public string AccessAddress = "127.0.0.1";
            /* This is the port used in the default region. */
            public uint AccessPort = 22102;
            /* The maximum number of players able to join this server instance. */
            public int MaxOnlinePlayers = 1000;

            /* Needed for authentication, and for some game systems. */
            public string WebServerUrl = "https://127.0.0.1:443";

            public LogOptions LogOptions = new();
            public GameOptions GameOptions = new();
            public JoinOptions JoinOptions = new();
            public ConsoleAccount ServerAccount = new();
        }

        /* Data containers. */

        public class LogOptions
        {
#if DEBUG
            public OpCode[] DebugWhitelist = Array.Empty<OpCode>();
            public OpCode[] DebugBlacklist = {
                OpCode.PingReq, 
                OpCode.PingRsp, 
                OpCode.PlayerSetPauseReq, 
                OpCode.PlayerSetPauseRsp, 
                OpCode.AbilityInvocationsNotify,
                OpCode.WorldPlayerRTTNotify, 
                OpCode.PlayerTimeNotify, 
                OpCode.UnionCmdNotify, 
                OpCode.CombatInvocationsNotify,
                OpCode.ClientAbilityChangeNotify, 
                OpCode.QueryPathReq, 
                OpCode.QueryPathRsp,
            };

            public bool KeepLog = true;
            public string LogLocation = Path.Combine("packetLogs");

            public bool LogCombatInvocations = true;
            public CombatTypeArgument[] CombatArgTypeBlacklist = { CombatTypeArgument.EntityMove };

            public bool LogAbilityInvocations = true;
            public AbilityInvokeArgument[] AbilityInvArgBlacklist = { AbilityInvokeArgument.MetaModifierDurabilityChange };
#endif

            /* Controls whether packets should be logged in console or not */
            public Shared.Enums.PacketDebugMode LogPackets = Shared.Enums.PacketDebugMode.BLACKLIST;

            /* Controls whether scene info, block, and group events should be logged in console or not */
            public Shared.Enums.SceneDebugMode LogScenes = Shared.Enums.SceneDebugMode.ERROR;

            /* Controls whether scene info, block, and group events should be logged in console or not */
            public Shared.Enums.AbilityDebugMode LogAbilities = Shared.Enums.AbilityDebugMode.ERROR;
        }

        public class GameOptions
        {
            public AvatarLimitsJson AvatarLimits = new();

            public bool WatchGachaConfig = false;
            public bool EnableShopItems = true;
            public bool StaminaUsage = true;
            public bool EnergyUsage = true;
            public bool FishhookTeleport = true;
            public ResinOptionsJson ResinOptions = new();
            public RatesJson Rates = new();
            public ConstantsJson Constants = new();

            public class ConstantsJson
            {
                public string VERSION = "3.2.0";

                public uint DEFAULT_TEAMS = 4;
                public uint MAX_TEAMS = 10;

                public uint MAX_FRIENDS = 45;
                public uint MAX_FRIEND_REQUESTS = 50;

                public uint SERVER_CONSOLE_UID = 99; // The UID of the server console's "player".

                public uint BATTLE_PASS_MAX_LEVEL = 50;
                public uint BATTLE_PASS_POINT_PER_LEVEL = 1000;
                public uint BATTLE_PASS_POINT_PER_WEEK = 10000;
                public uint BATTLE_PASS_LEVEL_PRICE = 150;
                public uint BATTLE_PASS_CURRENT_INDEX = 2;
            }

            public class AvatarLimitsJson
            {
                public uint SinglePlayerTeam = 4;
                public uint MultiplayerTeam = 4;
            }

            public class RatesJson
            {
                public float AdventureExp = 1.0f;
                public float Mora = 1.0f;
                public float LeyLines = 1.0f;
            }

            public class ResinOptionsJson
            {
                public bool ResinUsage = true;
                public uint Cap = 160;
                public uint RechargeTime = 480;
            }
        }

        public class JoinOptions
        {
            public uint[] WelcomeEmotes = { 2007, 1002, 4010 };
            public string WelcomeMessage = "Welcome to a Weedwacker server.";
            public Mail WelcomeMail = new();

            public class Mail
            {
                public string Title = "Welcome to Weedwacker!";
                public string Content = "Hi there!\r\nFirst of all, welcome to Weedwacker. If you have any issues, please let us know so that Primrose can help you!";
                public string Sender = "Primrose";
                /*
                public Weedwacker.game.mail.Mail.MailItem[] items = {
                new Weedwacker.game.mail.Mail.MailItem(13509, 1, 1),
                new Weedwacker.game.mail.Mail.MailItem(201, 99999, 1)
                };
                */
            }
        }

        public class ConsoleAccount
        {
            public uint AvatarId = 10000007;
            public uint NameCardId = 210001;
            public uint AdventureRank = 1;
            public uint WorldLevel = 0;

            public string NickName = "Server";
            public string Signature = "Welcome to Weedwacker!";
        }


        /* Objects. */

        public new class Region
        {
            public Region() { }

            public Region(
                string name, string title,
                string address, uint port
            )
            {
                Name = name;
                Title = title;
                Ip = address;
                Port = port;
            }

            public string Name = "os_usa";
            public string Title = "Weedwacker";
            public string Ip = "127.0.0.1";
            public uint Port = 22102;
        }
    }
}