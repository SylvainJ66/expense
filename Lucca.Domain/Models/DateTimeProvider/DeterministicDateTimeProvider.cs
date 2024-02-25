namespace Lucca.Domain.Models.DateTimeProvider;

public sealed class DeterministicDateTimeProvider : IDateTimeProvider
{
    public DateTime DateOfNow { get; set; } 
    public DateTime UtcNow()
    {
        return DateOfNow;
    }
}