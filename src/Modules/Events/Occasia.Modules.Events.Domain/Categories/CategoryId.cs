using Vogen;

namespace Occasia.Modules.Events.Domain.Categories;

[ValueObject<Guid>]
#pragma warning disable S1210
public readonly partial struct CategoryId
#pragma warning restore S1210
{
    public static CategoryId New => From(Guid.CreateVersion7());

    private static Validation Validate(Guid input)
    {
        return input == Guid.Empty ? Validation.Invalid("CategoryId cannot be empty.") : Validation.Ok;
    }
}
