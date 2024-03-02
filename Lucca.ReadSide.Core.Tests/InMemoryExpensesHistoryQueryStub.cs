using Lucca.ReadSide.Core.Gateways.Queries;
using Lucca.ReadSide.Core.Models;
using Lucca.ReadSide.Core.UseCases;

namespace Lucca.ReadSide.Core.Tests;

public class InMemoryExpensesHistoryQueryStub : IExpensesHistoryQuery
{
    public Dictionary<Guid, ExpensesHistoryReadModel> ExpensesByUserIds { get; } = new();
    
    public Task<ExpensesHistoryReadModel> ByUser(Guid userId, SortType sortType)
    {
        if(ExpensesByUserIds.TryGetValue(userId, out var expenses))
        {
            expenses.Expenses = sortType is SortType.Amount 
                ? expenses.Expenses.OrderBy(e => e.Amount).ToList() 
                : expenses.Expenses.OrderBy(e => e.ExpenseDate).ToList();
            
            expenses.NumberOfExpenses = expenses.Expenses.Count;
            
            return Task.FromResult(expenses);
        }

        return Task.FromResult(new ExpensesHistoryReadModel());
    }
}