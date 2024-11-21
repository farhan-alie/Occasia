using Occasia.Common.Domain;

namespace Occasia.Modules.Events.Domain.Events;

public sealed class EventCanceledDomainEvent(EventId eventId) : DomainEvent
{
    public EventId EventId { get; init; } = eventId;
}
