using Lucca.ReadSide.Core.Gateways.Queries;
using Lucca.ReadSide.Core.Models;
using Lucca.ReadSide.Core.UseCases;
using Lucca.Shared.Ressources.Bdd.Contexts;
using Lucca.Shared.Ressources.Bdd.EfModels;
using Microsoft.EntityFrameworkCore;

namespace Lucca.ReadSide.Infrastructure.Sql.Expenses;

public class SqlExpensesHistoryQuery : IExpensesHistoryQuery
{
    private readonly ExpenseDbContext _dbContext;

    public SqlExpensesHistoryQuery(ExpenseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ExpensesHistoryReadModel> ByUser(Guid userId, SortType sortType)
    {
        IQueryable<ExpenseEf> expensesQuery = _dbContext.Expenses
            .Where(e => e.UserId == userId);

        List<ExpenseEf> expenses;
        if(sortType is SortType.Amount)
        {
            expenses = await expensesQuery
                .OrderBy(e => e.Amount)
                .ToListAsync();
        }
        else
        {
            expenses = await expensesQuery
                .OrderBy(e => e.ExpenseDate)
                .ToListAsync();
        }

        var user = await _dbContext.Users
                       .FirstOrDefaultAsync(u => u.Id == userId) 
                   ?? throw new Exception("User not found");

        return new ExpensesHistoryReadModel(
            expenses.Count,
            [
                ..expenses.Select(e => new ExpenseReadModel(
                    e.Id,
                    e.UserId,
                    DisplayUser(user),
                    e.ExpenseType,
                    e.Amount,
                    e.ExpenseDate,
                    e.Currency,
                    e.Comment
                ))
            ]);
    }

    private static string DisplayUser(UserEf user)
    {
        return user.Firstname + " " + user.Name;
    }
}