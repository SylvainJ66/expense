using Lucca.Domain.Models.Expenses;

namespace Lucca.Domain.UseCases.CreateAnExpense;

public record struct CreateAnExpenseCommand(
    string UserId, 
    ExpenseType Type, 
    int Amount, 
    string Currency, 
    string Comment);