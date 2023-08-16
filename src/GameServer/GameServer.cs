using System.Net;
using System.Timers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Weedwacker.GameServer.Data;
using Weedwacker.GameServer.Data.BinOut.Ability.Temp;
using Weedwacker.GameServer.Data.BinOut.Avatar;
using Weedwacker.GameServer.Data.BinOut.Gadget;
using Weedwacker.GameServer.Data.BinOut.Shared.ConfigEntity;
using Weedwacker.GameServer.Systems.Avatar;
using Weedwacker.GameServer.Systems.Player;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Commands;
using Weedwacker.Shared.Network.Proto;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer;

internal class GameServer : BackgroundService
{
    private static readonly HttpClientHandler handler = new() { ServerCertificateCustomValidationCallback = delegate { return true; } };  //ignore ServerCertificate error
    private static readonly HttpClient client = new(handler);
    private static System.Timers.Timer? TickTimer;
    private ILogger<GameServer> Logger;
    public static GameConfig Configuration;
    public static SceneTreeCache TreeCache;
    public static SortedList<uint, Connection> OnlinePlayers = new(); // <gameUid,connection>
    private static HashSet<World> Worlds = new();
    public static SortedList<uint, AvatarCompiledData> AvatarInfo = new(); // <avatarId,data>
    public static Dictionary<uint, string> AbilityNameHashMap;

    private readonly ICommandHandler _commands;

    public GameServer(ICommandHandler commands, IEnumerable<IWeedwackerPlugin> plugins, ILogger<GameServer> logger)
    {
        _commands = commands;

        Logger = logger;
        //Logger.LogInformation("{Info}", "I'm working :DDDDDD");
        foreach (IWeedwackerPlugin plugin in plugins)
        {
            if (plugin is IConsoleHandler)
            {
                //Logger.LogInformation("{Info}", "Wooosh");
            }
        }
    }

    public static async Task<bool> VerifyToken(string accountUid, string token)
    {
        HttpResponseMessage rsp = await client.GetAsync($"{Configuration.Server.WebServerUrl}/extensions/combo/verify?uid={accountUid}&combo_token={token}");
        return rsp.StatusCode == HttpStatusCode.OK;
    }

    internal static void RegisterWorld(World world)
    {
        Worlds.Add(world);
    }

    public static AvatarCompiledData? GetAvatarInfo(uint avatarId)
    {
        if (AvatarInfo.TryGetValue(avatarId, out AvatarCompiledData? avatarInfo))
        {
            return avatarInfo;
        }
        else return null;
    }

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        //TODO
        try
        {
#if DEBUG
            if (!Directory.Exists(Configuration.Server.LogOptions.LogLocation))
                Directory.CreateDirectory(Configuration.Server.LogOptions.LogLocation);
            else
            {
                DirectoryInfo di = new(Configuration.Server.LogOptions.LogLocation);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
            }
#endif
            await GameData.LoadAllResourcesAsync(Configuration.structure.Resources);
            Crypto.LoadKeys(Configuration.structure.keys);
            await Database.DatabaseManager.Initialize();

            foreach (uint id in GameData.AvatarDataMap.Keys)
            {
                AvatarInfo.Add(id, new AvatarCompiledData(id));
            }
            SetAbilityHashMap();

            // Create a timer with a one second interval.
            TickTimer = new System.Timers.Timer(1000)
            {
                AutoReset = true,
                Enabled = true,
            };
            // Hook up the Elapsed event for the timer. 
            TickTimer.Elapsed += OnTick;

            Listener.StartListener();

            _commands.Start();
        }
        catch (Exception e)
        {
            Logger.LogError("{Error}", e.Message);
            //prevent zombie service and cleanly exit
            Environment.Exit(1);
        }
    }

    private static void SetAbilityHashMap()
    {
        Dictionary<uint, string> hashMap = new();
        foreach (ConfigAbility config in GameData.ConfigAbilityMap.Values)
        {
            hashMap[Utils.AbilityHash(config.abilityName)] = config.abilityName;
            if (config.abilitySpecials != null)
            {
                foreach (string special in config.abilitySpecials.Keys)
                {
                    hashMap[Utils.AbilityHash(special)] = special;
                }
            }
            if (config.modifiers != null)
            {
                foreach (string modifier in config.modifiers.Keys)
                {
                    hashMap[Utils.AbilityHash(modifier)] = modifier;
                }
            }
        }

        foreach (ConfigAvatar config in GameData.ConfigAvatarMap.Values)
        {
            if (config.abilities == null) continue;
            foreach (ConfigEntityAbilityEntry ability in config.abilities)
            {
                if (ability.abilityID != null)
                    hashMap[Utils.AbilityHash(ability.abilityID)] = ability.abilityID;
                if (ability.abilityName != null)
                    hashMap[Utils.AbilityHash(ability.abilityName)] = ability.abilityName;
            }
        }

        foreach (ConfigGadget config in GameData.ConfigGadgetMap.Values)
        {
            if (config.abilities == null) continue;
            foreach (ConfigEntityAbilityEntry? ability in config.abilities)
            {
                if (ability.abilityID != null)
                    hashMap[Utils.AbilityHash(ability.abilityID)] = ability.abilityID;
                if (ability.abilityName != null)
                    hashMap[Utils.AbilityHash(ability.abilityName)] = ability.abilityName;
            }
        }
        AbilityNameHashMap = hashMap;
    }

    private async void OnTick(object? source, ElapsedEventArgs e)
    {
        HashSet<World> toRemove = new();
        try
        {
            // Tick worlds.
            foreach (World world in Worlds)
            {
                if (await world.OnTickAsync())
                    toRemove.Add(world);
            }

            // Tick players.
            foreach (Connection con in OnlinePlayers.Values)
            {
                con.Player.OnTickAsync();
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "{Error}", "Tick event error");
        }
    }

    public static int GetShopNextRefreshTime(int shopType)
    {
        //TODO
        return int.MaxValue;
    }

    internal static async Task<SocialDetail?> GetSocialDetailByUid(uint askerUid, uint reqUid)
    {
        if (OnlinePlayers.TryGetValue(reqUid, out Connection session))
        {
            return session.Player.SocialManager.GetSocialDetail(askerUid);
        }
        else
        {
            Player? player = await Database.DatabaseManager.GetPlayerByGameUidAsync(reqUid);
            if (player != null)
                return player.SocialManager.GetSocialDetail(askerUid);
            else
                return null;
        }
    }
}
