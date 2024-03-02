using FluentAssertions;
using Lucca.Domain.Models.DateTimeProvider;
using Lucca.Domain.Models.Expenses;
using Lucca.Domain.Models.IdProvider;
using Lucca.Domain.Models.Users;
using Lucca.Infrastructure.Sql.Expenses;
using Lucca.Shared.Ressources.Bdd.EfModels;
using Xunit;

namespace Lucca.Infrastructure.Tests.Sql.Expenses;

public class SqlExpenseRepositoryTests : IClassFixture<IntegrationTestFixture>, IDisposable
{
    private readonly IntegrationTestFixture _fixture;
    private readonly DeterministicDateTimeProvider _dateTimeProvider = new();
    private readonly DeterministicIdProvider _idProvider = new();
    
    public SqlExpenseRepositoryTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
        _dateTimeProvider.DateOfNow = new DateTime(2024, 1, 2, 14, 15, 3);
        _idProvider.Id = Guid.Parse("c9e0f0d8-25e7-4224-8494-a802a7ac4f3e");
    }

    [Fact]
    public async Task Should_save_an_expense()
    {
        var user = User.Create(
            idProvider: _idProvider,
            firstname: "John", 
            name: "Doh", 
            currency: "EUR");
        await new SqlExpenseRepository(_fixture.ExpenseDbContext).Save(
            Expense.Create
            (
                idProvider: _idProvider,
                dateTimeProvider: _dateTimeProvider,
                userId: user.Id,
                expenseDate: new DateTime(2024, 1, 1, 00, 00, 00),
                type: ExpenseType.Restaurant,
                amount: 100,
                currency: "EUR",
                comment: "Lunch"
            ));
        var expectedExpenseEf = new ExpenseEf
        {
            Id = _idProvider.Id,
            ExpenseDate = new DateTime(2024, 1, 1, 00, 00, 00),
            CreatedAt = _dateTimeProvider.UtcNow(),
            ExpenseType = ExpenseType.Restaurant.ToString(),
            Amount = 100,
            Currency = "EUR",
            Comment = "Lunch",
            UserId = user.Id
        };

        var expenseEf = await _fixture.ExpenseDbContext.FindAsync<ExpenseEf>(_idProvider.Id);
        
        expenseEf.Should().BeEquivalentTo(expectedExpenseEf);
    }

    public void Dispose() 
        => _fixture.ResetDatabase();
}