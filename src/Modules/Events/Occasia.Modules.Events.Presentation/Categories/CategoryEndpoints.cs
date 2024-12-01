using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Occasia.Modules.Events.Presentation.Categories;

public static class CategoryEndpoints
{
    public static RouteGroupBuilder MapEndpoints(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup(Routes.Categories);
        CreateCategoryEndpoint.MapEndpoint(group);
        GetCategoriesEndpoint.MapEndpoint(group);
        group.WithTags("Categories API");
        return group;
    }
}
