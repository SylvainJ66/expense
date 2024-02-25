namespace Lucca.Domain.Models.IdProvider;

public class RealIdProvider : IIdProvider
{
    public Guid NewId()
    {
        return Guid.NewGuid();
    }
}