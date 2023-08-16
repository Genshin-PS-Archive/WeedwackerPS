using Weedwacker.Shared.Commands;
using Weedwacker.Shared.Commands.Receivers;
using Weedwacker.Shared.Enums;

namespace Weedwacker.GameServer.Commands.Receivers
{
    internal class PrivateChatReceiver : ICommandReceiver
    {
        private static readonly Lazy<PrivateChatReceiver> Lazy = new(() => new PrivateChatReceiver());
        public static PrivateChatReceiver Instance => Lazy.Value;

        private const UserRank Rank_ = UserRank.Player;

        public event EventHandler<CommandReceivedEventArgs>? CommandReceived;

        public void Start() { }

        public void Dispose() { }

        public string OnPrivateChatCommandReceived(string input)
        {
            StaticConsole output = new();
            CommandReceived?.Invoke(this, new CommandReceivedEventArgs(input, output, Rank_));

            return output.GetResult();
        }
    }
}
