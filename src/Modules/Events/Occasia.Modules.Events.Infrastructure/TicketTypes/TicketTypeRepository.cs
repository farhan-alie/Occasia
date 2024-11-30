using Microsoft.EntityFrameworkCore;
using Occasia.Modules.Events.Domain.Events;
using Occasia.Modules.Events.Domain.TicketTypes;
using Occasia.Modules.Events.Infrastructure.Data;

namespace Occasia.Modules.Events.Infrastructure.TicketTypes;

internal sealed class TicketTypeRepository(EventsDbContext context) : ITicketTypeRepository
{
    public async Task<TicketType?> GetAsync(TicketTypeId id, CancellationToken cancellationToken = default)
    {
        return await context.TicketTypes.FindAsync([id], cancellationToken);
    }

    public async Task<bool> ExistsAsync(EventId eventId, CancellationToken cancellationToken = default)
    {
        return await context.TicketTypes.AnyAsync(t => t.EventId == eventId, cancellationToken);
    }

    public void Insert(TicketType ticketType)
    {
        context.TicketTypes.Add(ticketType);
    }
}
