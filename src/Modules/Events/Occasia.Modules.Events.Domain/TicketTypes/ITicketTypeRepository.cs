using Occasia.Modules.Events.Domain.Events;

namespace Occasia.Modules.Events.Domain.TicketTypes;

public interface ITicketTypeRepository
{
    Task<TicketType?> GetAsync(TicketTypeId id, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(EventId eventId, CancellationToken cancellationToken = default);

    void Insert(TicketType ticketType);
}
