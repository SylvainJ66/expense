using Lucca.Domain.Gateways;
using Lucca.Domain.Models.Expenses;

namespace Lucca.Infrastructure.Sql.Expenses;

public class SqlExpenseRepository : IExpenseRepository
{
    public Task Save(Expense expense)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Expense>> GetBy(Guid userId, DateTime expenseDate, decimal amount)
    {
        throw new NotImplementedException();
    }
}