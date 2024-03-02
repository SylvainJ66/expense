using Lucca.ReadSide.Core.Gateways.Queries;
using Lucca.ReadSide.Core.Models;
using Lucca.ReadSide.Core.UseCases;
using Lucca.Shared.Ressources.Bdd.Contexts;

namespace Lucca.ReadSide.Infrastructure.Sql.Expenses;

public class SqlExpensesHistoryQuery : IExpensesHistoryQuery
{
    private readonly ExpenseDbContext _dbContext;
    
    public Task<ExpensesHistoryReadModel> ByUser(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<ExpensesHistoryReadModel> SortedBy(SortType sortType)
    {
        throw new NotImplementedException();
    }
}