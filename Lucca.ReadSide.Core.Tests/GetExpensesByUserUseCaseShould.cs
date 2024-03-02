using FluentAssertions;
using Lucca.ReadSide.Core.UseCases;

namespace Lucca.ReadSide.Core.Tests;

public class GetExpensesByUserUseCaseShould
{
    private readonly InMemoryExpensesHistoryQueryStub _expenseQuery = new();
    private readonly ExpenseReadModel _anExpense;
    private readonly ExpenseReadModel _anOtherExpense;

    public GetExpensesByUserUseCaseShould()
    {
        _anExpense = new ExpenseReadModel(
            Id: Guid.Parse("e9a1c841-2e6d-4853-8b51-c53896e49fab"),
            UserId: Guid.Parse("5b2928b9-cc09-4d33-9bb2-b9fc7d6a1a9a"),
            User: "Alice Liddell",
            Type: "Hotel",
            Amount: 100,
            ExpenseDate: DateTime.Parse("2022-01-01"),
            Currency: "EUR",
            Comment: "Comment");
        
        _anOtherExpense = new ExpenseReadModel(
            Id: Guid.Parse("a8815a61-9f9f-418a-9d16-aca5104fc5e7"),
            UserId: Guid.Parse("602447bf-c467-4b14-8f72-405125fe9a54"),
            User: "Bob Gepetto",
            Type: "Misc",
            Amount: 150,
            ExpenseDate: DateTime.Parse("2022-01-02"),
            Currency: "EUR",
            Comment: "An other comment");
    }
    
    [Fact]
    public async Task Return_an_empty_list_when_no_expenses()
    {
        var expenses = await new GetExpensesByUserUseCase(_expenseQuery)
            .Handle(Guid.Parse("00000000-0000-0000-0000-000000000000"));
        expenses.NumberOfExpenses.Should().Be(0);
    }
    
    [Fact]
    public async Task Return_expenses_history()
    {
       _expenseQuery.ExpensesByUserIds.Add(
           Guid.Parse("5b2928b9-cc09-4d33-9bb2-b9fc7d6a1a9a"),
           new ExpensesHistoryReadModel(1, [_anExpense]));
       
       
       _expenseQuery.ExpensesByUserIds.Add(
           Guid.Parse("a8815a61-9f9f-418a-9d16-aca5104fc5e7"),
           new ExpensesHistoryReadModel(1, [_anOtherExpense]));
        
        var expenses = await new GetExpensesByUserUseCase(_expenseQuery).Handle(Guid.Parse("5b2928b9-cc09-4d33-9bb2-b9fc7d6a1a9a"));
        
        expenses.NumberOfExpenses.Should().Be(1);
        expenses.Should().BeEquivalentTo(new ExpensesHistoryReadModel(
            1,
            [_anExpense]));
    }
}