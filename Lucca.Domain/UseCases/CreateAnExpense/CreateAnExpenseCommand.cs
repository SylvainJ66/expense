using Lucca.Domain.Models.Expenses;

namespace Lucca.Domain.UseCases.CreateAnExpense;

public record struct CreateAnExpenseCommand(
    Guid UserId, 
    ExpenseType Type, 
    decimal Amount, 
    DateTime ExpenseDate,
    string Currency, 
    string Comment);