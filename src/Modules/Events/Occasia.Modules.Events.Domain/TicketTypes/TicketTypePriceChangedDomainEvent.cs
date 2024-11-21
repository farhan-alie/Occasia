using Occasia.Common.Domain;

namespace Occasia.Modules.Events.Domain.TicketTypes;

public sealed class TicketTypePriceChangedDomainEvent(TicketTypeId ticketTypeId, decimal price) : DomainEvent
{
    public TicketTypeId TicketTypeId { get; init; } = ticketTypeId;

    public decimal Price { get; init; } = price;
}
