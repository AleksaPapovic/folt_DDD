using MediatR;

namespace FoltDelivery.Infrastructure.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
