namespace Lucca.Shared.Ressources.Extensions;

public static class DateTimeExtensions
{
    public static DateTime? SetKindUtc(this DateTime? dateTime)
    {
        return dateTime?.SetKindUtc();
    }

    public static DateTime SetKindUtc(this DateTime dateTime)
    {
        return dateTime.Kind is DateTimeKind.Utc 
            ? dateTime 
            : DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }
}