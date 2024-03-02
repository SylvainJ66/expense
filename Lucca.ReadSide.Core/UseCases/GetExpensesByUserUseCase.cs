using Lucca.ReadSide.Core.Gateways.Queries;
using Lucca.ReadSide.Core.Models;

namespace Lucca.ReadSide.Core.UseCases;


public class GetExpensesByUserUseCase
{
    private readonly IExpensesHistoryQuery _expensesHistoryQuery;

    public GetExpensesByUserUseCase(IExpensesHistoryQuery expensesHistoryQuery)
    {
        _expensesHistoryQuery = expensesHistoryQuery;
    }

    public async Task<ExpensesHistoryReadModel> Handle(
        Guid userId,
        SortType sortType)
    {
        var expenses = await _expensesHistoryQuery.ByUser(userId, sortType);
        return expenses.NumberOfExpenses > 0 
            ? expenses 
            : new ExpensesHistoryReadModel();
    }
}