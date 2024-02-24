namespace Lucca.Domain.Models.DateTimeProvider;

public class DeterministicDateTimeProvider : IDateTimeProvider
{
    public DateTime DateOfNow { get; set; } 
    public DateTime UtcNow()
    {
        return DateOfNow;
    }
}