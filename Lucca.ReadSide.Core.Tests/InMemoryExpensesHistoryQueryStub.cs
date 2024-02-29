using Lucca.ReadSide.Core.Gateways.Queries;
using Lucca.ReadSide.Core.UseCases;

namespace Lucca.ReadSide.Core.Tests;

public class InMemoryExpensesHistoryQueryStub : IExpensesHistoryQuery
{
    public Dictionary<Guid, ExpensesHistoryReadModel> ExpensesByUserIds { get; } = new();

    public Task<ExpensesHistoryReadModel> ByUser(Guid userId)
    {
        return Task.FromResult(ExpensesByUserIds.TryGetValue(userId, out var expenses) 
            ? expenses 
            : new ExpensesHistoryReadModel());
    }
}