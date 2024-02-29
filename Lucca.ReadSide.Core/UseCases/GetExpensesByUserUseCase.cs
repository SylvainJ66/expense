using Lucca.ReadSide.Core.Gateways.Queries;

namespace Lucca.ReadSide.Core.UseCases;


public class GetExpensesByUserUseCase
{
    private readonly IExpensesHistoryQuery _expensesHistoryQuery;

    public GetExpensesByUserUseCase(IExpensesHistoryQuery expensesHistoryQuery)
    {
        _expensesHistoryQuery = expensesHistoryQuery;
    }

    public async Task<ExpensesHistoryReadModel> Handle(Guid userId)
    {
        var expenses = await _expensesHistoryQuery.ByUser(userId);
        return expenses.NumberOfExpenses > 0 
            ? expenses 
            : new ExpensesHistoryReadModel();
    }
}