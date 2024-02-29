namespace Lucca.ReadSide.Core.UseCases;

public record struct ExpensesHistoryReadModel(
    int NumberOfExpenses, 
    List<ExpenseReadModel> Expenses);