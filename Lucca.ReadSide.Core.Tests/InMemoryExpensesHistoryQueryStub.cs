using Lucca.ReadSide.Core.Gateways.Queries;
using Lucca.ReadSide.Core.UseCases;

namespace Lucca.ReadSide.Core.Tests;

public class InMemoryExpensesHistoryQueryStub : IExpensesHistoryQuery
{
    public Dictionary<Guid, ExpensesHistoryReadModel> ExpensesByUserIds { get; set; } = new();

    public async Task<ExpensesHistoryReadModel> ByUser(Guid userId)
    {
        return ExpensesByUserIds.TryGetValue(userId, out var expenses) 
            ? expenses 
            : new ExpensesHistoryReadModel();
    }
}