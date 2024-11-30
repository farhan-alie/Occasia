using Occasia.Modules.Events.Domain.Categories;
using Occasia.Modules.Events.Domain.Events;
using Occasia.Modules.Events.Domain.TicketTypes;
using Vogen;

namespace Occasia.Modules.Events.Infrastructure.Data;

[EfCoreConverter<EventId>]
[EfCoreConverter<CategoryId>]
[EfCoreConverter<TicketTypeId>]
internal sealed partial class VogenEfCoreConverters;
