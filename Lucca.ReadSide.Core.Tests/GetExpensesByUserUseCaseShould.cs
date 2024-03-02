using FluentAssertions;
using Lucca.ReadSide.Core.Models;
using Lucca.ReadSide.Core.UseCases;

namespace Lucca.ReadSide.Core.Tests;

public class GetExpensesByUserUseCaseShould
{
    private readonly InMemoryExpensesHistoryQueryStub _expenseQuery = new();
    private readonly ExpenseReadModel _anExpense;
    private readonly ExpenseReadModel _anOtherExpense;
    private readonly ExpenseReadModel _aThirdExpense;
    private readonly ExpenseReadModel _aFourthExpense;
    private readonly Guid _aliceId = Guid.Parse("5b2928b9-cc09-4d33-9bb2-b9fc7d6a1a9a");
    private readonly Guid _bobId = Guid.Parse("602447bf-c467-4b14-8f72-405125fe9a54");

    public GetExpensesByUserUseCaseShould()
    {
        _anExpense = new ExpenseReadModel(
            Id: Guid.Parse("e9a1c841-2e6d-4853-8b51-c53896e49fab"),
            UserId: _aliceId,
            User: "Alice Liddell",
            Type: "Hotel",
            Amount: 100,
            ExpenseDate: DateTime.Parse("2022-01-01"),
            Currency: "EUR",
            Comment: "Comment");
        
        _anOtherExpense = new ExpenseReadModel(
            Id: Guid.Parse("a8815a61-9f9f-418a-9d16-aca5104fc5e7"),
            UserId: _bobId,
            User: "Bob Gepetto",
            Type: "Misc",
            Amount: 150,
            ExpenseDate: DateTime.Parse("2022-01-02"),
            Currency: "EUR",
            Comment: "An other comment");
        
        _aThirdExpense = new ExpenseReadModel(
            Id: Guid.Parse("a8815a61-9f9f-418a-9d16-aca5104fc5e7"),
            UserId: _bobId,
            User: "Bob Gepetto",
            Type: "Misc",
            Amount: 140,
            ExpenseDate: DateTime.Parse("2022-04-02"),
            Currency: "EUR",
            Comment: "A third comment");
        
        _aFourthExpense = new ExpenseReadModel(
            Id: Guid.Parse("a8815a61-9f9f-418a-9d16-aca5104fc5e7"),
            UserId: _bobId,
            User: "Bob Gepetto",
            Type: "Misc",
            Amount: 90,
            ExpenseDate: DateTime.Parse("2022-03-02"),
            Currency: "EUR",
            Comment: "A third comment");
    }
    
    [Fact]
    public async Task Return_an_empty_list_when_no_expenses()
    {
        var expenses = await new GetExpensesByUserUseCase(_expenseQuery)
            .Handle(
                Guid.Parse("00000000-0000-0000-0000-000000000000"),
                SortType.Amount);
        expenses.NumberOfExpenses.Should().Be(0);
    }
    
    [Fact]
    public async Task Return_expenses_history()
    {
       _expenseQuery.ExpensesByUserIds.Add(
           _aliceId,
           new ExpensesHistoryReadModel(1, [_anExpense]));
       _expenseQuery.ExpensesByUserIds.Add(
           Guid.Parse("a8815a61-9f9f-418a-9d16-aca5104fc5e7"),
           new ExpensesHistoryReadModel(1, [_anOtherExpense]));
        
        var expenses = await new GetExpensesByUserUseCase(_expenseQuery)
            .Handle(_aliceId, SortType.Amount);
        
        expenses.NumberOfExpenses.Should().Be(1);
        expenses.Should().BeEquivalentTo(new ExpensesHistoryReadModel(
            1,
            [_anExpense]));
    }
    
    [Fact]
    public async Task Return_expenses_sorted_by_date()
    {
        _expenseQuery.ExpensesByUserIds.Add(
            _bobId,
            new ExpensesHistoryReadModel(1, [_anOtherExpense, _aThirdExpense, _aFourthExpense]));
        
        var expenses = await new GetExpensesByUserUseCase(_expenseQuery)
            .Handle(_bobId, SortType.Date);
        
        expenses.Should().BeEquivalentTo(new ExpensesHistoryReadModel(
            3,
            [_aFourthExpense, _aThirdExpense, _anOtherExpense]));
        expenses.Expenses.Should().BeInAscendingOrder(e => e.ExpenseDate);
    }
    
    [Fact]
    public async Task Return_expenses_sorted_by_amount()
    {
        _expenseQuery.ExpensesByUserIds.Add(
            _bobId,
            new ExpensesHistoryReadModel(1, [_anOtherExpense, _aThirdExpense, _aFourthExpense]));
        
        var expenses = await new GetExpensesByUserUseCase(_expenseQuery)
            .Handle(_bobId, SortType.Amount);
        
        expenses.Should().BeEquivalentTo(new ExpensesHistoryReadModel(
            3,
            [_aFourthExpense, _aThirdExpense, _anOtherExpense]));
        expenses.Expenses.Should().BeInAscendingOrder(e => e.Amount);
    }
    
    [Fact]
    public async Task Return_formated_expense()
    {
        _expenseQuery.ExpensesByUserIds.Add(
            _aliceId,
            new ExpensesHistoryReadModel(1, [_anExpense]));

        var expenses = await new GetExpensesByUserUseCase(_expenseQuery)
            .Handle(_aliceId, SortType.Date);

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
}