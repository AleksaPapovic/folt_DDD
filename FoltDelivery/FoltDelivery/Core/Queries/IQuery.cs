using MediatR;

namespace FoltDelivery.Core.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse> { }
}
