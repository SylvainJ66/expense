using Lucca.ReadSide.Core.Models;
using Lucca.ReadSide.Core.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Lucca.ReadSide.Api;

[ApiController]
[Route("expenses")]
public class ExpensesReadSideController : Controller
{
    private readonly GetExpensesByUserUseCase _getExpensesByUserUseCase;

    public ExpensesReadSideController(
        GetExpensesByUserUseCase getExpensesByUserUseCase)
    {
        _getExpensesByUserUseCase = getExpensesByUserUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> ByUserId(
        [FromQuery] Guid userId,
        [FromQuery] SortType sortType)
    {
        return Ok(await _getExpensesByUserUseCase.Handle(userId, sortType));
    }
}