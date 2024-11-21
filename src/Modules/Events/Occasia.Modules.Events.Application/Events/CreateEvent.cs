using ErrorOr;
using FluentValidation;
using Occasia.Common.Application.Clock;
using Occasia.Common.Application.Messaging;
using Occasia.Modules.Events.Application.Abstractions.Data;
using Occasia.Modules.Events.Domain.Categories;
using Occasia.Modules.Events.Domain.Events;

namespace Occasia.Modules.Events.Application.Events;

public static class CreateEvent
{
    public sealed record Command(
        CategoryId CategoryId,
        string Title,
        string Description,
        string Location,
        DateTime StartsAtUtc,
        DateTime? EndsAtUtc) : ICommand<ErrorOr<EventId>>;

    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Location).NotEmpty();
            RuleFor(x => x.StartsAtUtc).NotEmpty();
            RuleFor(x => x.EndsAtUtc).GreaterThan(x => x.StartsAtUtc).When(x => x.EndsAtUtc.HasValue);
        }
    }

    internal sealed class Handler(
        IEventRepository eventRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<Command, ErrorOr<EventId>>
    {
        public async Task<ErrorOr<EventId>> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.StartsAtUtc < dateTimeProvider.UtcNow)
            {
                return EventErrors.StartDateInPast;
            }

            Category? category = await categoryRepository.GetAsync(request.CategoryId, cancellationToken);

            if (category is null)
            {
                return CategoryErrors.NotFound(request.CategoryId);
            }

            ErrorOr<Event> @event = Event.Create(
                category,
                request.Title,
                request.Description,
                request.Location,
                request.StartsAtUtc,
                request.EndsAtUtc);

            if (@event.IsError)
            {
                return @event.Errors;
            }

            eventRepository.Insert(@event.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return @event.Value.Id;
        }
    }
}
