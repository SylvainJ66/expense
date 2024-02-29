using Lucca.ReadSide.Core.Gateways.Queries;
using Lucca.ReadSide.Core.UseCases;

namespace Lucca.ReadSide.Core.Tests;

public class InMemoryExpensesHistoryQueryStub : IExpensesHistoryQuery
{
    public Dictionary<Guid, ExpensesHistoryReadModel> ExpensesByUserIds { get; set; } = new();

    public async Task<ExpensesHistoryReadModel> ByUser(Guid userId)
    {
        return await Task.FromResult(ExpensesByUserIds[userId]);
    }
}