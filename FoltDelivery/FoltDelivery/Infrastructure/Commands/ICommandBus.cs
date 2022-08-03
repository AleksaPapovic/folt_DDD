using System.Threading.Tasks;

namespace FoltDelivery.Infrastructure.Commands
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
