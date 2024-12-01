using Occasia.Modules.Events.Domain.Categories;

namespace Occasia.Modules.Events.Application.Categories;

public sealed record CategoryResponse(CategoryId Id, string Name, bool IsArchived);
