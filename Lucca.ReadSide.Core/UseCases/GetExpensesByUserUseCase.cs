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

public record struct ExpensesHistoryReadModel(int NumberOfExpenses, List<ExpenseReadModel> Expenses);

public record struct ExpenseReadModel(
    Guid Id,
    Guid UserId,
    string Type,
    decimal Amount,
    DateTime ExpenseDate,
    string Currency,
    string Comment);