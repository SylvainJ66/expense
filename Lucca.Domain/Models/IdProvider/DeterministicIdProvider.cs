namespace Lucca.Domain.Models.IdProvider;

public class DeterministicIdProvider : IIdProvider
{
    public Guid Id { get; set; }
    public Guid NewId()
    {
        return Id;
    }
}