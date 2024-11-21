using ErrorOr;
using Occasia.Common.Domain;
using Occasia.Modules.Events.Domain.Categories;

namespace Occasia.Modules.Events.Domain.Events;

public sealed class Event : Entity
{
    private Event()
    {
    }

    public EventId Id { get; private set; }

    public CategoryId CategoryId { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public string Location { get; private set; }

    public DateTime StartsAtUtc { get; private set; }

    public DateTime? EndsAtUtc { get; private set; }

    public EventStatus Status { get; private set; }

    public static ErrorOr<Event> Create(
        Category category,
        string title,
        string description,
        string location,
        DateTime startsAtUtc,
        DateTime? endsAtUtc)
    {
        if (endsAtUtc.HasValue && endsAtUtc < startsAtUtc)
        {
            return EventErrors.EndDatePrecedesStartDate;
        }

        Event @event = new()
        {
            Id = EventId.New,
            CategoryId = category.Id,
            Title = title,
            Description = description,
            Location = location,
            StartsAtUtc = startsAtUtc,
            EndsAtUtc = endsAtUtc,
            Status = EventStatus.Draft
        };

        @event.Raise(new EventCreatedDomainEvent(@event.Id));

        return @event;
    }

    public ErrorOr<Success> Publish()
    {
        if (Status != EventStatus.Draft)
        {
            return EventErrors.NotDraft;
        }

        Status = EventStatus.Published;

        Raise(new EventPublishedDomainEvent(Id));

        return Result.Success;
    }

    public void Reschedule(DateTime startsAtUtc, DateTime? endsAtUtc)
    {
        if (StartsAtUtc == startsAtUtc && EndsAtUtc == endsAtUtc)
        {
            return;
        }

        StartsAtUtc = startsAtUtc;
        EndsAtUtc = endsAtUtc;

        Raise(new EventRescheduledDomainEvent(Id, StartsAtUtc, EndsAtUtc));
    }

    public ErrorOr<Success> Cancel(DateTime utcNow)
    {
        if (Status == EventStatus.Canceled)
        {
            return EventErrors.AlreadyCanceled;
        }

        if (StartsAtUtc < utcNow)
        {
            return EventErrors.AlreadyStarted;
        }

        Status = EventStatus.Canceled;

        Raise(new EventCanceledDomainEvent(Id));

        return Result.Success;
    }
}
