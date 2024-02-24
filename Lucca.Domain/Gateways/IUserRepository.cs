using Lucca.Domain.Models.Users;

namespace Lucca.Domain.Gateways;

public interface IUserRepository
{
    Task<User?> GetBy(string userId);
}