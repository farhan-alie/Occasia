using Microsoft.EntityFrameworkCore;
using Occasia.Modules.Events.Application.Abstractions.Data;
using Occasia.Modules.Events.Domain.Categories;
using Occasia.Modules.Events.Domain.Events;
using Occasia.Modules.Events.Domain.TicketTypes;
using Occasia.Modules.Events.Infrastructure.Events;
using Occasia.Modules.Events.Infrastructure.TicketTypes;

namespace Occasia.Modules.Events.Infrastructure.Data;

public sealed class EventsDbContext : DbContext, IUnitOfWork
{
    public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options)
    {
    }

    internal DbSet<Event> Events { get; set; }

    internal DbSet<Category> Categories { get; set; }

    internal DbSet<TicketType> TicketTypes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Events);

        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new TicketTypeConfiguration());
    }

    protected override void ConfigureConventions(
        ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.RegisterAllInVogenEfCoreConverters();
    }
}
