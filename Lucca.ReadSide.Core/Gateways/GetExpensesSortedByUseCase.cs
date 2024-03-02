using Lucca.ReadSide.Core.Gateways.Queries;
using Lucca.ReadSide.Core.Models;
using Lucca.ReadSide.Core.UseCases;

namespace Lucca.ReadSide.Core.Gateways;

public class GetExpensesSortedByUseCase
{
    private readonly IExpensesHistoryQuery _expensesHistoryQuery;
    
    public GetExpensesSortedByUseCase(
        IExpensesHistoryQuery expensesHistoryQuery)
    {
        _expensesHistoryQuery = expensesHistoryQuery;
    }
    
    public async Task<ExpensesHistoryReadModel> Handle(SortType sort)
    {
        var expenses = await _expensesHistoryQuery.SortedBy(sort);
        return expenses.NumberOfExpenses > 0 
            ? expenses 
            : new ExpensesHistoryReadModel();
    }
    
}