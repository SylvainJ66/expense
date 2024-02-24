namespace Lucca.Domain.Models.IdProvider;

public class DeterministicIdProvider : IIdProvider
{
    public string Id { get; set; }
    public string NewId()
    {
        return Id;
    }
}