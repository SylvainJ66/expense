namespace Lucca.ReadSide.Core.UseCases;

public record struct ExpenseReadModel(
    Guid Id,
    Guid UserId,
    string Type,
    decimal Amount,
    DateTime ExpenseDate,
    string Currency,
    string Comment);