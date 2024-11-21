using Occasia.Common.Domain;

namespace Occasia.Modules.Events.Domain.Categories;

public sealed class CategoryNameChangedDomainEvent(CategoryId categoryId, string name) : DomainEvent
{
    public CategoryId CategoryId { get; init; } = categoryId;

    public string Name { get; init; } = name;
}
