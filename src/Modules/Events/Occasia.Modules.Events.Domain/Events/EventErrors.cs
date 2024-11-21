using ErrorOr;

namespace Occasia.Modules.Events.Domain.Events;

public static class EventErrors
{
    public static readonly Error StartDateInPast = Error.Validation(
        "Events.StartDateInPast",
        "The event start date is in the past");

    public static readonly Error EndDatePrecedesStartDate = Error.Validation(
        "Events.EndDatePrecedesStartDate",
        "The event end date precedes the start date");

    public static readonly Error NoTicketsFound = Error.Validation(
        "Events.NoTicketsFound",
        "The event does not have any ticket types defined");

    public static readonly Error NotDraft = Error.Validation("Events.NotDraft", "The event is not in draft status");


    public static readonly Error AlreadyCanceled = Error.Validation(
        "Events.AlreadyCanceled",
        "The event was already canceled");


    public static readonly Error AlreadyStarted = Error.Validation(
        "Events.AlreadyStarted",
        "The event has already started");

    public static Error NotFound(Guid eventId)
    {
        return Error.NotFound("Events.NotFound", $"The event with the identifier {eventId} was not found");
    }
}
