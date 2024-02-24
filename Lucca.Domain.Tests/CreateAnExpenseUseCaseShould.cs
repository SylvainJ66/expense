using FluentAssertions;
using Lucca.Domain.Models;
using Lucca.Domain.Models.DateTimeProvider;
using Lucca.Domain.Models.Expenses;
using Lucca.Domain.UseCases.CreateAnExpense;

namespace Lucca.Domain.Tests;

public class CreateAnExpenseUseCaseShould
{
    private readonly InMemoryExpenseRepositoryStub _expenseRepository = new();
    private readonly DeterministicDateTimeProvider _dateTimeProvider = new();
    
    [Fact]
    public async Task CreateAnExpense()
    {
        _dateTimeProvider.DateOfNow = new DateTime(2024, 1, 2, 14, 15, 3);
        
        var command = new CreateAnExpenseCommand
        (
            UserId: "user-id",
            Type: ExpenseType.Restaurant,
            Amount: 100,
            Currency: "EUR",
            Comment: "Lunch"
        );
        
        await new CreateAnExpenseUseCase(_expenseRepository, _dateTimeProvider).Handle(command);
        
        _expenseRepository.Expenses.Should().BeEquivalentTo(new List<Expense>
        {
            new
            (
                UserId: "user-id",
                CreatedAt: _dateTimeProvider.UtcNow(),
                Type: ExpenseType.Restaurant,
                Amount: 100,
                Currency: "EUR",
                Comment: "Lunch"
            )
        });
    }
}