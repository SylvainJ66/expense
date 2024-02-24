using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lucca.Domain.Models.Users.Exceptions;

internal class UserNotFoundExeption : ProblemDetailsException
{
    public UserNotFoundExeption() : base(new ProblemDetails
    {
        Title = "User not found",
        Status = StatusCodes.Status400BadRequest,
        Detail = "User not found"
    })
    {
    }
}