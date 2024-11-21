using Vogen;

namespace Occasia.Modules.Events.Domain.Events;

[ValueObject<Guid>]
#pragma warning disable S1210
public readonly partial struct EventId
#pragma warning restore S1210
{
    public static EventId New => From(Guid.CreateVersion7());

    private static Validation Validate(Guid input)
    {
        return input == Guid.Empty ? Validation.Invalid("EventId cannot be empty.") : Validation.Ok;
    }
}
