namespace Lucca.ReadSide.Core.UseCases;

public class GetExpensesByUserUseCase
{
    public async Task<ExpensesHistoryReadModel> Handle(Guid userId)
    {
        return new ExpensesHistoryReadModel();
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