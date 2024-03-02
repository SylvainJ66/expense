using Lucca.Domain.Gateways;
using Lucca.Domain.Models.Users;
using Lucca.Shared.Ressources.Bdd.Contexts;

namespace Lucca.Infrastructure.Sql.Users;

public class SqlUserRepository : IUserRepository
{
    private readonly ExpenseDbContext _dbContext;

    public SqlUserRepository(ExpenseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetBy(Guid userId)
    {
        var userEf = await _dbContext.Users.FindAsync(userId);
        
        if(userEf is null)
            return null;
        
        return User.From(
            userEf.Id, userEf.Name, userEf.Firstname, userEf.Currency);
    }
}