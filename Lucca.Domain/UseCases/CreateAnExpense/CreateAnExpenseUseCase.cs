using Lucca.Domain.Gateways;
using Lucca.Domain.Models;
using Lucca.Domain.Models.DateTimeProvider;

namespace Lucca.Domain.UseCases.CreateAnExpense;

public class CreateAnExpenseUseCase
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateAnExpenseUseCase(
        IExpenseRepository expenseRepository, 
        IDateTimeProvider dateTimeProvider)
    {
        _expenseRepository = expenseRepository;
        _dateTimeProvider = dateTimeProvider;
    }
        
    public async Task Handle(CreateAnExpenseCommand command)
    {
        await _expenseRepository.Save(new Expense
        (
            UserId: command.UserId,
            CreatedAt: _dateTimeProvider.UtcNow(),
            Type: command.Type,
            Amount: command.Amount,
            Currency: command.Currency,
            Comment: command.Comment
        ));
    }
}