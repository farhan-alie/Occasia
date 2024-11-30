using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Occasia.Modules.Events.Domain.Categories;
using Occasia.Modules.Events.Domain.Events;

namespace Occasia.Modules.Events.Infrastructure.Events;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasOne<Category>().WithMany();
    }
}
