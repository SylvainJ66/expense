using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lucca.Domain.Models.Expenses.Exceptions;

public class ExpenseWithNoCommentException : ProblemDetailsException
{
    public ExpenseWithNoCommentException() : base(new ProblemDetails
    {
        Title = "Expense should have a comment",
        Status = StatusCodes.Status400BadRequest,
        Detail = "Expense should have a comment",
    })
    {
    }
}