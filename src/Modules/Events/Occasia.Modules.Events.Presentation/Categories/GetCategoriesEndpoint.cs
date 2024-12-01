using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Occasia.Modules.Events.Application.Categories;
using Occasia.Modules.Events.Presentation.ApiResults;

namespace Occasia.Modules.Events.Presentation.Categories;

internal static class GetCategoriesEndpoint
{
    public static void MapEndpoint(RouteGroupBuilder group)
    {
        group.MapGet("", static async (ISender sender) =>
        {
            var query = new GetCategories.Query();
            ErrorOr<IReadOnlyList<CategoryResponse>> result = await sender.Send(query);
            return result.ToResult();
        });
    }
}
