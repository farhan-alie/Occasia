using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Occasia.Modules.Events.Application.Categories;
using Occasia.Modules.Events.Domain.Categories;
using Occasia.Modules.Events.Presentation.ApiResults;

namespace Occasia.Modules.Events.Presentation.Categories;

internal static class CreateCategoryEndpoint
{
    public static void MapEndpoint(RouteGroupBuilder group)
    {
        group.MapPost("", async (Request request, ISender sender) =>
        {
            var command = new CreateCategory.Command(request.Name);
            ErrorOr<CategoryId> result = await sender.Send(command);
            return result.ToResult();
        });
    }

    internal sealed record Request(string Name);
}
