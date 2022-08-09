using System.Diagnostics;

namespace FoltDelivery.Infrastructure.Events
{

    public record EventMetadata(
        string EventId,
        ulong StreamPosition,
        ulong LogPosition
    );


    public interface IEventEnvelope
    {
        object Data { get; }
        EventMetadata Metadata { get; set; }
    }
}
