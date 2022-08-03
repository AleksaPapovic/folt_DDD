using System.Diagnostics;
using FoltDelivery.Infrastructure.Tracing;

namespace FoltDelivery.Infrastructure.Events
{

    public record EventMetadata(
        string EventId,
        ulong StreamPosition,
        ulong LogPosition,
        TraceMetadata? Trace
    );


    public interface IEventEnvelope
    {
        object Data { get; }
        EventMetadata Metadata { get; set; }
    }
}
