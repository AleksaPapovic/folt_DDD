using System;
using System.Threading.Tasks;

namespace FoltDelivery.Infrastructure.Events
{
    public interface ITransientEventSubscriber
    {
        void Subscribe<T>(Action<T> handler);

        void Subscribe<T>(Func<T, Task> handler);
    }
}
