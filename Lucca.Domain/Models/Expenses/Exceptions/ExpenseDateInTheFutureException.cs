using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lucca.Domain.Models.Expenses.Exceptions;

public class ExpenseDateInTheFutureException : ProblemDetailsException
{
    public ExpenseDateInTheFutureException() : base(new ProblemDetails
    {
        Title = "Expense date cannot be in the future",
        Status = StatusCodes.Status400BadRequest,
        Detail = "The expense date cannot be in the future"
    })
    {
    }
}