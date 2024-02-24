namespace Lucca.Domain.Models.Expenses;

public record Expense(
    string UserId, 
    DateTime ExpenseDate,
    DateTime CreatedAt, 
    object Type, 
    int Amount, 
    string Currency, 
    string Comment);