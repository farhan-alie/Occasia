using Occasia.Common.Domain;
using Occasia.Modules.Events.Domain.Events;

namespace Occasia.Modules.Events.Domain.TicketTypes;

public sealed class TicketType : Entity
{
    private TicketType()
    {
    }

    public TicketTypeId Id { get; private set; }

    public EventId EventId { get; private set; }

    public string Name { get; private set; }

    public decimal Price { get; private set; }

    public string Currency { get; private set; }

    public decimal Quantity { get; private set; }

    public static TicketType Create(
        Event @event,
        string name,
        decimal price,
        string currency,
        decimal quantity)
    {
        var ticketType = new TicketType
        {
            Id = TicketTypeId.New,
            EventId = @event.Id,
            Name = name,
            Price = price,
            Currency = currency,
            Quantity = quantity
        };

        return ticketType;
    }

    public void UpdatePrice(decimal price)
    {
        if (Price == price)
        {
            return;
        }

        Price = price;

        Raise(new TicketTypePriceChangedDomainEvent(Id, Price));
    }
}
