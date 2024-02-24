using Lucca.Domain.Gateways;
using Lucca.Domain.Models.Expenses;

namespace Lucca.Domain.Tests;

public class InMemoryExpenseRepositoryStub : IExpenseRepository
{
    public IEnumerable<Expense> Expenses { get; private set; } = new List<Expense>();
    public Task Save(Expense expense)
    {
        ((List<Expense>)Expenses).Add(expense);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Expense>> GetBy(
        string userId, 
        DateTime expenseDate, 
        decimal amount)
    {
        return Task.FromResult(
            Expenses.Where(expense => 
                expense.UserId == userId && 
                expense.ExpenseDate == expenseDate && 
                expense.Amount == amount));
    }

    public Task FeedWith(Expense expense)
    {
        ((List<Expense>)Expenses).Add(expense);
        return Task.CompletedTask;
    }
}