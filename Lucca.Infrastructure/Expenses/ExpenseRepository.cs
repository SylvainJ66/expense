using Lucca.Domain.Gateways;
using Lucca.Domain.Models.Expenses;

namespace Lucca.Infrastructure.Expenses;

public class ExpenseRepository : IExpenseRepository
{
    public Task Save(Expense expense)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Expense>> GetBy(string userId, DateTime expenseDate, decimal amount)
    {
        throw new NotImplementedException();
    }
}