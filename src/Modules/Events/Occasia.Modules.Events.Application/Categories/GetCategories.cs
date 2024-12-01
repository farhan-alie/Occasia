using System.Data.Common;
using Dapper;
using ErrorOr;
using Occasia.Common.Application.Data;
using Occasia.Common.Application.Messaging;

namespace Occasia.Modules.Events.Application.Categories;

public static class GetCategories
{
    public sealed record Query : IQuery<ErrorOr<IReadOnlyList<CategoryResponse>>>;


    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<Query, ErrorOr<IReadOnlyList<CategoryResponse>>>
    {
        public async Task<ErrorOr<IReadOnlyList<CategoryResponse>>> Handle(Query request,
            CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync().ConfigureAwait(false);

            const string sql =
                $"""
                 SELECT
                     id AS {nameof(CategoryResponse.Id)},
                     name AS {nameof(CategoryResponse.Name)},
                     is_archived AS {nameof(CategoryResponse.IsArchived)}
                 FROM events.categories
                 """;

            var categories = (await connection.QueryAsync<CategoryResponse>(sql, request)).ToList();

            return categories;
        }
    }
}
