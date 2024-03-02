using FluentAssertions;
using Lucca.ReadSide.Core.Gateways;
using Lucca.ReadSide.Core.Models;
using Lucca.ReadSide.Core.UseCases;

namespace Lucca.ReadSide.Core.Tests;

public class GetExpensesSortedByUseCaseShould
{
    private readonly InMemoryExpensesHistoryQueryStub _expenseQuery = new();
    private readonly ExpenseReadModel _anExpense;
    private readonly ExpenseReadModel _anOtherExpense;
    private readonly ExpenseReadModel _aThirdExpense;
    private const string _aliceId = "5b2928b9-cc09-4d33-9bb2-b9fc7d6a1a9a";
    private const string _bobId = "602447bf-c467-4b14-8f72-405125fe9a54";

    public GetExpensesSortedByUseCaseShould()
    {
        _anExpense = new ExpenseReadModel(
            Id: Guid.Parse("e9a1c841-2e6d-4853-8b51-c53896e49fab"),
            UserId: Guid.Parse(_aliceId),
            User: "Alice Liddell",
            Type: "Hotel",
            Amount: 100,
            ExpenseDate: DateTime.Parse("2022-01-01"),
            Currency: "EUR",
            Comment: "Comment");
        
        _anOtherExpense = new ExpenseReadModel(
            Id: Guid.Parse("0efd47bf-e0fb-4500-a00e-54882def9d74"),
            UserId: Guid.Parse(_bobId),
            User: "Bob Gepetto",
            Type: "Misc",
            Amount: 90,
            ExpenseDate: DateTime.Parse("2022-01-03"),
            Currency: "EUR",
            Comment: "An other comment");
        
        _aThirdExpense = new ExpenseReadModel(
            Id: Guid.Parse("a8815a61-9f9f-418a-9d16-aca5104fc5e7"),
            UserId: Guid.Parse(_bobId),
            User: "Bob Gepetto",
            Type: "Misc",
            Amount: 150,
            ExpenseDate: DateTime.Parse("2022-01-02"),
            Currency: "EUR",
            Comment: "A third comment");
    }
    
    [Fact]
    public async Task Return_expenses_sorted_by_amount()
    {
        FeedWithExpenses();
        
        var expenses = await new GetExpensesSortedByUseCase(_expenseQuery)
            .Handle(SortType.Amount);
        
        expenses.Should().BeEquivalentTo(new ExpensesHistoryReadModel(
            3,
            [_aThirdExpense, _anExpense, _anOtherExpense]));
        expenses.Expenses.Should().BeInAscendingOrder(e => e.Amount);
    }
    
    [Fact]
    public async Task Return_expenses_sorted_by_date()
    {
        FeedWithExpenses();

        var expenses = await new GetExpensesSortedByUseCase(_expenseQuery)
            .Handle(SortType.Date);
        
        expenses.Should().BeEquivalentTo(new ExpensesHistoryReadModel(
            3,
            [_anExpense, _aThirdExpense, _anOtherExpense]));
        expenses.Expenses.Should().BeInAscendingOrder(e => e.ExpenseDate);
    }
    
    [Fact]
    public async Task Return_formated_expense()
    {
        _expenseQuery.ExpensesByUserIds.Add(
            Guid.Parse(_aliceId),
            new ExpensesHistoryReadModel(1, [_anExpense]));

        var expenses = await new GetExpensesSortedByUseCase(_expenseQuery)
            .Handle(SortType.Date);

        expenses.Expenses.First().Should().BeEquivalentTo(new
        {
            Id = _anExpense.Id,
            UserId = _anExpense.UserId,
            User = "Alice Liddell",
            Type = _anExpense.Type,
            Amount = _anExpense.Amount,
            ExpenseDate = _anExpense.ExpenseDate,
            Currency = _anExpense.Currency,
            Comment = _anExpense.Comment
        });
    }

    private void FeedWithExpenses()
    {
        _expenseQuery.ExpensesByUserIds.Add(
            Guid.Parse(_aliceId),
            new ExpensesHistoryReadModel(1, [_anExpense]));
        _expenseQuery.ExpensesByUserIds.Add(
            Guid.Parse(_bobId),
            new ExpensesHistoryReadModel(1, [_anOtherExpense, _aThirdExpense]));
    }
}