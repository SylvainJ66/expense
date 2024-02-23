using FluentAssertions;

namespace Lucca.Domain.Tests;

public class CreateAnExpenseUseCaseShould
{
    private readonly InMemoryExpenseRepositoryStub _expenseRepository = new InMemoryExpenseRepositoryStub();
    
    [Fact]
    public async Task CreateAnExpense()
    {
        var command = new CreateAnExpenseCommand
        (
            UserId: "user-id",
            Type: ExpenseType.Restaurant,
            Amount: 100,
            Currency: "EUR",
            Comment: "Lunch"
        );
        
        await new CreateAnExpenseUseCase().Handle(command);
        
        _expenseRepository.Expenses.Should().BeEquivalentTo(new List<Expense>
        {
            new
            (
                UserId: "user-id",
                CreatedAt: DateTime.UtcNow,
                Type: ExpenseType.Restaurant,
                Amount: 100,
                Currency: "EUR",
                Comment: "Lunch"
            )
        });
    }
}

internal class InMemoryExpenseRepositoryStub : IExpenseRepository
{
    public IEnumerable<Expense> Expenses { get; } = new List<Expense>();
    public Task Save(Expense expense)
    {
        throw new NotImplementedException();
    }
}

internal interface IExpenseRepository
{
    Task Save(Expense expense);
}

public class CreateAnExpenseUseCase
{
    public async Task Handle(CreateAnExpenseCommand command)
    {
        
    }
}

public record struct CreateAnExpenseCommand(
    string UserId, 
    ExpenseType Type, 
    int Amount, 
    string Currency, 
    string Comment);

public enum ExpenseType
{
    Restaurant,
    Hotel,
    Misc
}

public record Expense(
    string UserId, 
    DateTime CreatedAt, 
    object Type, 
    int Amount, 
    string Currency, 
    string Comment);