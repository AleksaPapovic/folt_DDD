using System.Diagnostics;

namespace FoltDelivery.Core.Events
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
