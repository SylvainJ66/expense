using Lucca.Domain.Models;

namespace Lucca.Domain.Gateways;

public interface IExpenseRepository
{
    Task Save(Expense expense);
}