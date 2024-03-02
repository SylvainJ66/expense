using Lucca.ReadSide.Core.Gateways.Queries;
using Lucca.ReadSide.Core.Models;
using Lucca.ReadSide.Core.UseCases;

namespace Lucca.ReadSide.Core.Tests;

public class InMemoryExpensesHistoryQueryStub : IExpensesHistoryQuery
{
    public Dictionary<Guid, ExpensesHistoryReadModel> ExpensesByUserIds { get; } = new();
    
    public Task<ExpensesHistoryReadModel> ByUser(Guid userId)
    {
        return Task.FromResult(ExpensesByUserIds.TryGetValue(userId, out var expenses) 
            ? expenses 
            : new ExpensesHistoryReadModel());
    }

    public Task<ExpensesHistoryReadModel> SortedBy(SortType sortType)
    {
        List<ExpenseReadModel> expenses;

        if (sortType is SortType.Amount)
        {
            expenses = ExpensesByUserIds
                .Values
                .SelectMany(e => e.Expenses)
                .OrderBy(e => e.Amount)
                .ToList();
        }
        else
        {
            expenses = ExpensesByUserIds
                .Values
                .SelectMany(e => e.Expenses)
                .OrderBy(e => e.ExpenseDate)
                .ToList();
        }
        
        var result = new ExpensesHistoryReadModel(
            expenses.Count,
            expenses);

        return Task.FromResult(result);
    }
}