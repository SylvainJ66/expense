using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lucca.Domain.Models.Expenses.Exceptions;

public class ExpenseCannotBeMadeTwiceException : ProblemDetailsException
{
    public ExpenseCannotBeMadeTwiceException() : base(new ProblemDetails
    {
        Title = "Expense cannot be made twice",
        Status = StatusCodes.Status400BadRequest,
        Detail = "Expense cannot be made twice"
        
    })
    {
    }
}