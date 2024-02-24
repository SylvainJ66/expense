namespace Lucca.Domain.Models.DateTimeProvider;

public interface IDateTimeProvider
{
    DateTime UtcNow();
}