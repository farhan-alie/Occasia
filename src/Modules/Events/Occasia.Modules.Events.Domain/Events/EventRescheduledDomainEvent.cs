using Occasia.Common.Domain;

namespace Occasia.Modules.Events.Domain.Events;

public sealed class EventRescheduledDomainEvent(EventId eventId, DateTime startsAtUtc, DateTime? endsAtUtc)
    : DomainEvent
{
    public EventId EventId { get; } = eventId;

    public DateTime StartsAtUtc { get; } = startsAtUtc;

    public DateTime? EndsAtUtc { get; } = endsAtUtc;
}
