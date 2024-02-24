using Lucca.Domain.Models.Expenses;

namespace Lucca.Domain.Gateways;

public interface IExpenseRepository
{
    Task Save(Expense expense);
    Task<IEnumerable<Expense>> GetBy(
        string userId, 
        DateTime expenseDate, 
        int amount);
}