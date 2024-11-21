using Occasia.Common.Domain;

namespace Occasia.Modules.Events.Domain.Categories;

public sealed class CategoryArchivedDomainEvent(CategoryId categoryId) : DomainEvent
{
    public CategoryId CategoryId { get; init; } = categoryId;
}
