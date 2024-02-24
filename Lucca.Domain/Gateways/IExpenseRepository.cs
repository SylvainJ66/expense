using Lucca.Domain.Models;
using Lucca.Domain.Models.Expenses;

namespace Lucca.Domain.Gateways;

public interface IExpenseRepository
{
    Task Save(Expense expense);
}