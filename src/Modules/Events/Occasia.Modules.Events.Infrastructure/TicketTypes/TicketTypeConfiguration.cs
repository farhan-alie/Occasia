using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Occasia.Modules.Events.Domain.Events;
using Occasia.Modules.Events.Domain.TicketTypes;

namespace Occasia.Modules.Events.Infrastructure.TicketTypes;

internal sealed class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
{
    public void Configure(EntityTypeBuilder<TicketType> builder)
    {
        builder.HasOne<Event>().WithMany().HasForeignKey(t => t.EventId);
    }
}
