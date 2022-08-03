using MediatR;

namespace FoltDelivery.Infrastructure.Commands
{
    public interface ICommandHandler<in T> : IRequestHandler<T>
        where T : ICommand
    {
    }
}
