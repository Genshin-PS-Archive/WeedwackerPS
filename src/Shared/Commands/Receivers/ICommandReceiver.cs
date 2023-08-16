namespace Weedwacker.Shared.Commands.Receivers
{
    public interface ICommandReceiver : IDisposable
    {
        event EventHandler<CommandReceivedEventArgs> CommandReceived;

        void Start();
    }
}
