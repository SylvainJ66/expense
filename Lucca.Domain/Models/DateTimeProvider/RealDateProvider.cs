namespace Lucca.Domain.Models.DateTimeProvider;

public class RealDateProvider : IDateTimeProvider
{
    public DateTime UtcNow()
    {
        return DateTime.UtcNow;
    }
}