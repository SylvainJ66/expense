using Lucca.Domain.Gateways;
using Lucca.Domain.Models;
using Lucca.Domain.Models.Expenses;

namespace Lucca.Domain.Tests;

public class InMemoryExpenseRepositoryStub : IExpenseRepository
{
    public IEnumerable<Expense> Expenses { get; } = new List<Expense>();
    public Task Save(Expense expense)
    {
        ((List<Expense>)Expenses).Add(expense);
        return Task.CompletedTask;
    }
}