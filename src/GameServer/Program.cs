using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Weedwacker.GameServer.Commands.Definitions;
using Weedwacker.GameServer.Commands.Receivers;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Commands;
using Weedwacker.Shared.Commands.Configuration;
using Weedwacker.Shared.Commands.Definitions;
using Weedwacker.Shared.Commands.Receivers;
using Weedwacker.Shared.Utils.Configuration;

namespace Weedwacker.GameServer;

public static class Program
{
    public static void Main(string[] args)
    {
        GameServer.Configuration = Config.Load<GameConfig>("GameConfig.json").Result;
        GameServer.TreeCache = new SceneTreeCache(Path.Combine(GameServer.Configuration.structure.Cache, "grids"));

        Dictionary<string, Type> types = new();
        if (Directory.Exists("Plugins"))
            foreach (string path in Directory.GetFiles("Plugins").Where(w => w.EndsWith(".dll")))
            {
                foreach (Type t in Assembly.LoadFrom(path).GetTypes().Where(w => !w.IsInterface && !w.IsAbstract && typeof(IWeedwackerPlugin).IsAssignableFrom(w)))
                {
                    types.Add(t.FullName, t);
                }
            }

        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((builder) =>
            {
                builder.AddJsonFile("GameConfig.json", optional: false);
            })
            .ConfigureLogging((ILoggingBuilder builder) =>
            {

            })
            .ConfigureServices((context, services) =>
            {
                // Add command system
                services.AddSingleton<ICommandHandler, CommandHandler>();

                services.AddSingleton<ICommandReceiver, CommandLineReceiver>();
                services.AddSingleton<ICommandReceiver>(PrivateChatReceiver.Instance);

                IConfigurationSection tcpReceiverConfig = context.Configuration.GetSection(TcpReceiverConfig.ConfigKey);
                if (tcpReceiverConfig.Exists())
                {
                    services.Configure<TcpReceiverConfig>(tcpReceiverConfig);
                    services.AddSingleton<ICommandReceiver, TcpReceiver>();
                }

                services.AddSingleton<ICommandDefinition, ExtendedDebugCommandDefinition>();
                services.AddSingleton<ICommandDefinition, AvatarCommandDefinition>();
                services.AddSingleton<ICommandDefinition, GiveCommandDefinition>();
                services.AddSingleton<ICommandDefinition, SpawnCommandDefinition>();
                services.AddSingleton<ICommandDefinition, ListCommandDefinitions>();

                // Add plugins and their configuration
                foreach (Type t in types.Values)
                {
                    Type? setType = types[$"{t.FullName}Settings"];
                    //No configuration required. Create only a single instance
                    if (setType == null)
                    {
                        services.AddSingleton<IWeedwackerPlugin>((IServiceProvider serv) =>
                        {
                            return (IWeedwackerPlugin)ActivatorUtilities.CreateInstance(serv, t);
                        });
                        continue;
                    }
                    IEnumerable<IConfigurationSection> sections = context.Configuration.GetSection(t.FullName).GetChildren();
                    foreach (IConfigurationSection section in sections)
                    {
                        object? settings = section.Get(setType);
                        services.AddSingleton<IWeedwackerPlugin>((IServiceProvider serv) =>
                        {
                            return (IWeedwackerPlugin)ActivatorUtilities.CreateInstance(serv, t, settings);
                        });
                    }
                }

                // Add game server host
                services.AddHostedService<GameServer>();
            })
            .Build();

        host.RunAsync().Wait();
    }
}


public interface IWeedwackerPlugin
{

}

//example
public interface IConsoleHandler : IWeedwackerPlugin
{

}
