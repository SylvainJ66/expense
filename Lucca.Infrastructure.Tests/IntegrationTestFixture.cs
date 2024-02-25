using Lucca.Infrastructure.Sql.Contexts;
using Lucca.Shared.Tests;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Lucca.Infrastructure.Tests;

public class IntegrationTestFixture : IAsyncLifetime
{
    private readonly PgTestContainerRunner _pgTestContainerRunner = new();
    public ExpenseDbContext ExpenseDbContext { get; private set; } = null!;
    
    public async Task InitializeAsync()
    {
        await _pgTestContainerRunner.Init();
        ExpenseDbContext = NewDbContextInstance();
    }

    public async Task DisposeAsync()
    {
        await _pgTestContainerRunner.Down();
    }

    private ExpenseDbContext NewDbContextInstance()
    {
        return new ExpenseDbContext(
            new DbContextOptionsBuilder().UseNpgsql(_pgTestContainerRunner.ConnectionString()).Options);
    }

    public void ResetDatabase()
    {
        ExpenseDbContext.Users.RemoveRange(ExpenseDbContext.Users);
        ExpenseDbContext.Expenses.RemoveRange(ExpenseDbContext.Expenses);
    }
}