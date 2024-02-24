namespace Lucca.Domain.Models;

public record Expense(
    string UserId, 
    DateTime CreatedAt, 
    object Type, 
    int Amount, 
    string Currency, 
    string Comment);