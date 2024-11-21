using Vogen;

namespace Occasia.Modules.Events.Domain.TicketTypes;

[ValueObject<Guid>]
#pragma warning disable S1210
public readonly partial struct TicketTypeId
#pragma warning restore S1210
{
    public static TicketTypeId New => From(Guid.CreateVersion7());

    private static Validation Validate(Guid input)
    {
        return input == Guid.Empty ? Validation.Invalid("TicketTypeId cannot be empty.") : Validation.Ok;
    }
}
