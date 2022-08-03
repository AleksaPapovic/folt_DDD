using FoltDelivery.Infrastructure.Tracing.Causation;
using FoltDelivery.Infrastructure.Tracing.Correlation;

namespace FoltDelivery.Infrastructure.Tracing
{
    public record TraceMetadata(
        CorrelationId? CorrelationId,
        CausationId? CausationId
    );

    public interface ITraceMetadataProvider
    {
        TraceMetadata? Get();
    }

    public class TraceMetadataProvider : ITraceMetadataProvider
    {
        private readonly ICorrelationIdProvider correlationIdProvider;
        private readonly ICausationIdProvider causationIdProvider;

        public TraceMetadataProvider(
            ICorrelationIdProvider correlationIdProvider,
            ICausationIdProvider causationIdProvider
        )
        {
            this.correlationIdProvider = correlationIdProvider;
            this.causationIdProvider = causationIdProvider;
        }

        public TraceMetadata? Get()
        {
            var correlationId = correlationIdProvider.Get();
            var causationId = causationIdProvider.Get();

            if (correlationId == null && causationId == null)
                return null;

            return new TraceMetadata(correlationId, causationId);
        }
    }
}