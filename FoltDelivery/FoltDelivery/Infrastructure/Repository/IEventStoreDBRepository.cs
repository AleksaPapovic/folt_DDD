using FoltDelivery.Infrastructure.Aggregate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoltDelivery.Infrastructure.Repository
{
    public interface IEventStoreDBRepository<T> where T : class, IAggregate
    {
        Task<T?> Find(Guid id, CancellationToken cancellationToken);

        Task<ulong> Add(
            T aggregate,
            CancellationToken ct = default
        );

        Task<ulong> Update(
            T aggregate,
            ulong? expectedRevision = null,
            CancellationToken ct = default
        );

        Task<ulong> Delete(
            T aggregate,
            ulong? expectedRevision = null,
            CancellationToken ct = default
        );
    }
}
