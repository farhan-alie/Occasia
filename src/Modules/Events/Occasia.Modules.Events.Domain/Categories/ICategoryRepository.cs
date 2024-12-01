namespace Occasia.Modules.Events.Domain.Categories;

public interface ICategoryRepository
{
    Task<IReadOnlyCollection<Category>> ListAsync(CancellationToken cancellationToken = default);
    Task<Category?> GetAsync(CategoryId id, CancellationToken cancellationToken = default);
    void Insert(Category category);
}
