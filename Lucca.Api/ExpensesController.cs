using Lucca.Domain.Models.Expenses;
using Lucca.Domain.UseCases.CreateAnExpense;
using Microsoft.AspNetCore.Mvc;

namespace Lucca.Api;

[ApiController]
[Route("expenses")]
public class ExpensesController : Controller
{
    private readonly CreateAnExpenseUseCase _createAnExpenseUseCase;

    public ExpensesController(CreateAnExpenseUseCase createAnExpenseUseCase)
    {
        _createAnExpenseUseCase = createAnExpenseUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAnExpense(
        [FromBody] ExpensePostBody expensePostBody)
    {
        await _createAnExpenseUseCase.Handle(new CreateAnExpenseCommand(
            UserId:Guid.Parse(expensePostBody.UserId),
            Type: Enum.Parse<ExpenseType>(expensePostBody.Type),
            Amount: expensePostBody.Amount,
            ExpenseDate: expensePostBody.ExpenseDate,
            Currency: expensePostBody.Currency,
            Comment: expensePostBody.Comment
        ));
        return Created("expense", "");
    }
}

public record struct ExpensePostBody(
    string UserId,
    string Type,
    decimal Amount,
    DateTime ExpenseDate,
    string Currency,
    string Comment);

