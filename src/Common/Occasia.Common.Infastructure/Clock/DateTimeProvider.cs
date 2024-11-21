using Occasia.Common.Application.Clock;

namespace Occasia.Common.Infastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
