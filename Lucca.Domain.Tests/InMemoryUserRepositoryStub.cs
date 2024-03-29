using Lucca.Domain.Gateways;
using Lucca.Domain.Models.Users;

namespace Lucca.Domain.Tests;

internal class InMemoryUserRepositoryStub : IUserRepository
{
    public IEnumerable<User> Users { get; } = new List<User>();
    
    public Task<User?> GetBy(Guid userId)
    {
        
        return Task.FromResult(
            Users.FirstOrDefault(user => user.Id == userId));
    }
    
    public void FeedWith(User user)
    {
        ((List<User>)Users).Add(user);
    }
}