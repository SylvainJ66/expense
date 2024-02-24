using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lucca.Domain.Models.Expenses.Exceptions;

public class ExpenseAndUserCannotHaveDifferentCurrency : ProblemDetailsException
{
    public ExpenseAndUserCannotHaveDifferentCurrency() : base(new ProblemDetails
    {
        Title = "Expense and user cannot have different currency",
        Status = StatusCodes.Status400BadRequest,
        Detail = "Expense and the User cannot have different currency"
    })
    {
    }
}