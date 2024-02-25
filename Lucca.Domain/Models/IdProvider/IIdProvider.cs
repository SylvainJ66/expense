namespace Lucca.Domain.Models.IdProvider;

public interface IIdProvider
{
    Guid NewId();
}