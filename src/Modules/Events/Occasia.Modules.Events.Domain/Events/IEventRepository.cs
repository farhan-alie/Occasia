namespace Occasia.Modules.Events.Domain.Events;

public interface IEventRepository
{
    Task<Event?> GetAsync(EventId id, CancellationToken cancellationToken = default);

    void Insert(Event @event);
}
