using ErrorOr;
using Occasia.Common.Application.Messaging;
using Occasia.Modules.Events.Domain.Categories;

namespace Occasia.Modules.Events.Application.Categories;

public static class GetCategories
{
    public sealed record Query : IQuery<ErrorOr<IReadOnlyList<CategoryResponse>>>;


    internal sealed class Handler(ICategoryRepository categoryRepository)
        : IQueryHandler<Query, ErrorOr<IReadOnlyList<CategoryResponse>>>
    {
        public async Task<ErrorOr<IReadOnlyList<CategoryResponse>>> Handle(Query request,
            CancellationToken cancellationToken)
        {
            IReadOnlyCollection<Category> categories =
                await categoryRepository.ListAsync(cancellationToken).ConfigureAwait(false);

            return categories.Select(c => new CategoryResponse(c.Id, c.Name, c.IsArchived)).ToList();
        }
    }
}
