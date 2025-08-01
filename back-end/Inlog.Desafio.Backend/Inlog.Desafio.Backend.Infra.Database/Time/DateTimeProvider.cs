using SharedKernel;

namespace Inlog.Desafio.Backend.Infra.Database.Time;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}