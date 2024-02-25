using Lucca.Infrastructure.Sql.Contexts;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Xunit;

namespace Lucca.Infrastructure.Tests;

public class IntegrationTestFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .Build();

    public ExpenseDbContext ExpenseDbContext { get; private set; } = null!;
    
    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();
        ExpenseDbContext = NewDbContextInstance();
    }

    public async Task DisposeAsync()
    {
        await _postgres.DisposeAsync().AsTask();
    }

    private ExpenseDbContext NewDbContextInstance()
    {
        return new ExpenseDbContext(
            new DbContextOptionsBuilder().UseNpgsql(_postgres.GetConnectionString()).Options);
    }

    public void ResetDatabase()
    {
        ExpenseDbContext.Users.RemoveRange(ExpenseDbContext.Users);
        ExpenseDbContext.Expenses.RemoveRange(ExpenseDbContext.Expenses);
    }
}