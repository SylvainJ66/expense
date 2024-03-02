using Lucca.Domain.Gateways;
using Lucca.Domain.Models.Expenses;
using Lucca.Shared.Ressources.Bdd.Contexts;
using Lucca.Shared.Ressources.Bdd.EfModels;

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
        return Task.FromResult(_dbContext.Expenses
            .Where(e => e.UserId == userId)
            .Where(e => e.ExpenseDate == expenseDate)
            .Where(e => e.Amount == amount)
            .Select(e => Expense.From
            (
                e.Id,
                e.UserId,
                e.ExpenseDate,
                e.CreatedAt,
                Enum.Parse<ExpenseType>(e.ExpenseType),
                e.Amount,
                e.Currency,
                e.Comment
            )).AsEnumerable());
    }
}