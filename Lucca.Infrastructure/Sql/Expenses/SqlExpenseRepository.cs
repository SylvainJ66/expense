using Lucca.Domain.Gateways;
using Lucca.Domain.Models.Expenses;
using Lucca.Infrastructure.Sql.Contexts;
using Lucca.Infrastructure.Sql.EfModels;

namespace Lucca.Infrastructure.Sql.Expenses;

public class SqlExpenseRepository : IExpenseRepository
{
    private readonly ExpenseDbContext _dbContext;

    public SqlExpenseRepository(ExpenseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Save(Expense expense)
    {
        _dbContext.Expenses.Add(new ExpenseEf
        {
            Id = expense.Id,
            ExpenseDate = expense.ExpenseDate,
            CreatedAt = expense.CreatedAt,
            ExpenseType = expense.Type.ToString(),
            Amount = expense.Amount,
            Currency = expense.Currency,
            Comment = expense.Comment,
            UserId = expense.UserId
        });
        await _dbContext.SaveChangesAsync();
    }

    public Task<IEnumerable<Expense>> GetBy(Guid userId, DateTime expenseDate, decimal amount)
    {
        throw new NotImplementedException();
    }
}