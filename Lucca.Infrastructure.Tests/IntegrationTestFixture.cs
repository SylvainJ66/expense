using Lucca.Shared.Ressources.Bdd.Contexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var sqlScriptPath = Path.Combine(baseDirectory, "Bdd", "expense-ddl.sql");
        await ExecuteSqlScriptFromFileAsync(_postgres.GetConnectionString(), sqlScriptPath);
    }

    public async Task DisposeAsync()
    {
        await _postgres.DisposeAsync().AsTask();
    }
    
    public void ResetDatabase()
    {
        ExpenseDbContext.Users.RemoveRange(ExpenseDbContext.Users);
        ExpenseDbContext.Expenses.RemoveRange(ExpenseDbContext.Expenses);
        ExpenseDbContext.SaveChanges();
    }

    private ExpenseDbContext NewDbContextInstance()
    {
        return new ExpenseDbContext(
            new DbContextOptionsBuilder().UseNpgsql(_postgres.GetConnectionString()).Options);
    }
    
    private static async Task ExecuteSqlScriptFromFileAsync(string connectionString, string filePath)
    {
        var script = await File.ReadAllTextAsync(filePath);

        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();
        await using var command = new NpgsqlCommand(script, connection);
        await command.ExecuteNonQueryAsync();
    }
}