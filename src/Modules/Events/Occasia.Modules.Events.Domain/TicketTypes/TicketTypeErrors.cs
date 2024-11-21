using ErrorOr;

namespace Occasia.Modules.Events.Domain.TicketTypes;

public static class TicketTypeErrors
{
    public static Error NotFound(TicketTypeId ticketTypeId)
    {
        return Error.NotFound("TicketTypes.NotFound",
            $"The ticket type with the identifier {ticketTypeId} was not found");
    }
}
