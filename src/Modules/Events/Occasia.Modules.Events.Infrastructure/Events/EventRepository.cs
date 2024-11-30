using Occasia.Modules.Events.Domain.Events;
using Occasia.Modules.Events.Infrastructure.Data;

namespace Occasia.Modules.Events.Infrastructure.Events;

public class EventRepository(EventsDbContext context) : IEventRepository
{
    public async Task<Event?> GetAsync(EventId id, CancellationToken cancellationToken = default)
    {
        return await context.Events.FindAsync([id], cancellationToken);
    }

    public void Insert(Event @event)
    {
        context.Events.Add(@event);
    }
}
