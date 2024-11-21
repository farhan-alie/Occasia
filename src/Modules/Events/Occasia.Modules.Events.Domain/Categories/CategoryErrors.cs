using ErrorOr;

namespace Occasia.Modules.Events.Domain.Categories;

public static class CategoryErrors
{
    public static readonly Error AlreadyArchived = Error.Validation(
        "Categories.AlreadyArchived",
        "The category was already archived");

    public static Error NotFound(CategoryId categoryId)
    {
        return Error.NotFound("Categories.NotFound", $"The category with the identifier {categoryId} was not found");
    }
}
