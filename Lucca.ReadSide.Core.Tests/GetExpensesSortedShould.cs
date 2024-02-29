using Lucca.ReadSide.Core.UseCases;

namespace Lucca.ReadSide.Core.Tests;

public class GetExpensesSortedShould
{
    private readonly InMemoryExpensesHistoryQueryStub _expenseQuery = new();
    private readonly ExpenseReadModel _anExpense;
    private readonly ExpenseReadModel _anOtherExpense;

    public GetExpensesSortedShould()
    {
        _anExpense = new ExpenseReadModel(
            Id: Guid.Parse("e9a1c841-2e6d-4853-8b51-c53896e49fab"),
            UserId: Guid.Parse("5b2928b9-cc09-4d33-9bb2-b9fc7d6a1a9a"),
            Type: "Hotel",
            Amount: 100,
            ExpenseDate: DateTime.Parse("2022-01-01"),
            Currency: "EUR",
            Comment: "Comment");
        
        _anOtherExpense = new ExpenseReadModel(
            Id: Guid.Parse("a8815a61-9f9f-418a-9d16-aca5104fc5e7"),
            UserId: Guid.Parse("602447bf-c467-4b14-8f72-405125fe9a54"),
            Type: "Misc",
            Amount: 150,
            ExpenseDate: DateTime.Parse("2022-01-02"),
            Currency: "EUR",
            Comment: "An other comment");
    }
    
    [Fact]
    public async Task Return_expenses_sorted_by_amount()
    {
        throw new NotImplementedException();
    }
}