using System.Reflection;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Npgsql;

namespace Lucca.Shared.Tests;

public class PgTestContainerRunner
{
    public TestcontainerDatabase Container { get; } = new TestcontainersBuilder<PostgreSqlTestcontainer>()
        .WithDatabase(new PostgreSqlTestcontainerConfiguration
        {
            Database = "expense-db",
            Username = "postgres",
            Password = "postgres",
        })
        .WithCleanUp(true)
        .Build();
    
    public string ConnectionString()
    {
        return Container.ConnectionString;
    }

    public async Task Init()
    {
        await Container.StartAsync();
        await RunDdlScriptToDatabase();
    }
    
    public async Task Down()
    {
        await Container.DisposeAsync();
    }

    private async Task RunDdlScriptToDatabase()
    {
        await using var stream = await LoadDdlSqlFile();
        using var reader = new StreamReader(stream ?? throw new InvalidOperationException());
        var sql = await reader.ReadToEndAsync();
        await using var connection = new NpgsqlConnection(Container.ConnectionString);
        connection.Open();
        await using var command = new NpgsqlCommand(sql, connection);
        await Task.Run(() => command.ExecuteNonQuery());
    }

    private static Task<Stream?> LoadDdlSqlFile()
    {
        const string resourceName = "Shared.Write_Side.Resources.expense-ddl.sql";
        var assembly = Assembly.LoadFrom("Shared.dll");
        return Task.FromResult(assembly.GetManifestResourceStream(resourceName));
    }
}