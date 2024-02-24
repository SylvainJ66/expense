using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lucca.Domain.Models.Expenses.Exceptions;

public class ExpenseDateMoreThanThreeMonthsInThePastException : ProblemDetailsException
{
    public ExpenseDateMoreThanThreeMonthsInThePastException() : base(new ProblemDetails
    {
        Title = "Expense date cannot be more than three months in the past",
        Status = StatusCodes.Status400BadRequest,
        Detail = "Expense date cannot be more than three months in the past",
    })
    {
    }
}