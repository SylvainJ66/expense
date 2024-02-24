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

    public CreateAnExpenseUseCaseShould()
    {
        _dateTimeProvider.DateOfNow = new DateTime(2024, 1, 2, 14, 15, 3);
    }
    
    [Fact]
    public async Task CreateAnExpense()
    {
        var command = new CreateAnExpenseCommand
        (
            UserId: "user-id",
            Type: ExpenseType.Restaurant,
            Amount: 100,
            ExpenseDate: new DateTime(2024, 1, 1, 00, 00, 00),
            Currency: "EUR",
            Comment: "Lunch"
        );
        
        await new CreateAnExpenseUseCase(_expenseRepository, _dateTimeProvider).Handle(command);
        
        _expenseRepository.Expenses.Should().BeEquivalentTo(new List<Expense>
        {
            new
            (
                UserId: "user-id",
                ExpenseDate: new DateTime(2024, 1, 1, 00, 00, 00),
                CreatedAt: _dateTimeProvider.UtcNow(),
                Type: ExpenseType.Restaurant,
                Amount: 100,
                Currency: "EUR",
                Comment: "Lunch"
            )
        });
    }

    [Fact]
    public async Task Avoid_date_in_the_future()
    {
        var command = new CreateAnExpenseCommand
        (
            UserId: "user-id",
            ExpenseDate: new DateTime(2025, 1, 2, 00, 00, 00),
            Type: ExpenseType.Restaurant,
            Amount: 100,
            Currency: "EUR",
            Comment: "Lunch"
        );
        
        var action = () => new CreateAnExpenseUseCase(_expenseRepository, _dateTimeProvider).Handle(command);
        
        await action.Should().ThrowExactlyAsync<ExpenseDateInTheFutureException>()
            .WithMessage(" : Expense date cannot be in the future");
    }
}