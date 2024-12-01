using Microsoft.EntityFrameworkCore;
using Occasia.Modules.Events.Domain.Categories;
using Occasia.Modules.Events.Infrastructure.Data;

namespace Occasia.Modules.Events.Infrastructure.Categories;

internal sealed class CategoryRepository(EventsDbContext context) : ICategoryRepository
{
    public async Task<IReadOnlyCollection<Category>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await context.Categories.ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetAsync(CategoryId id, CancellationToken cancellationToken = default)
    {
        return await context.Categories.FindAsync([id], cancellationToken);
    }

    public void Insert(Category category)
    {
        context.Categories.Add(category);
    }
}
