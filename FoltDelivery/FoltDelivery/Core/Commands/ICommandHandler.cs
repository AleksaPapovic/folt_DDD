using MediatR;

namespace FoltDelivery.Core.Commands
{
    public interface ICommandHandler<in T> : IRequestHandler<T> where T : ICommand { }
}
