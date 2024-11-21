namespace Occasia.Modules.Events.Domain.Categories;

public interface ICategoryRepository
{
    Task<Category?> GetAsync(CategoryId id, CancellationToken cancellationToken = default);

    void Insert(Category category);
}
