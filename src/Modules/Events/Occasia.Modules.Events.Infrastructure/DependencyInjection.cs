using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Occasia.Modules.Events.Application.Abstractions.Data;
using Occasia.Modules.Events.Domain.Categories;
using Occasia.Modules.Events.Domain.Events;
using Occasia.Modules.Events.Domain.TicketTypes;
using Occasia.Modules.Events.Infrastructure.Categories;
using Occasia.Modules.Events.Infrastructure.Data;
using Occasia.Modules.Events.Infrastructure.Events;
using Occasia.Modules.Events.Infrastructure.TicketTypes;

namespace Occasia.Modules.Events.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddDbContext<EventsDbContext>(options =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
                .AddInterceptors());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}
