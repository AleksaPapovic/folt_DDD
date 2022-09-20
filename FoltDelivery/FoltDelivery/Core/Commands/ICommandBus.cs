using System.Threading.Tasks;

namespace FoltDelivery.Core.Commands
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
