using Lucca.ReadSide.Core.UseCases;

namespace Lucca.ReadSide.Core.Gateways.Queries;

public interface IExpensesHistoryQuery
{
    Task<ExpensesHistoryReadModel> ByUser(Guid userId);
}