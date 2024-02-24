using Lucca.Domain.Gateways;
using Lucca.Domain.Models;
using Lucca.Domain.Models.DateTimeProvider;
using Lucca.Domain.Models.Expenses;
using Lucca.Domain.Tests;

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
        ThrowIfDateIsInTheFuture(command.ExpenseDate);
        ThrowIfDateIsMoreThanThreeMonthsInThePast(command.ExpenseDate);
        
        await _expenseRepository.Save(new Expense
        (
            UserId: command.UserId,
            ExpenseDate: command.ExpenseDate,
            CreatedAt: _dateTimeProvider.UtcNow(),
            Type: command.Type,
            Amount: command.Amount,
            Currency: command.Currency,
            Comment: command.Comment
        ));
    }

    private void ThrowIfDateIsInTheFuture(DateTime commandExpenseDate)
    {
        if(commandExpenseDate > _dateTimeProvider.UtcNow())
            throw new ExpenseDateInTheFutureException();
    }
    
    private void ThrowIfDateIsMoreThanThreeMonthsInThePast(DateTime commandExpenseDate)
    {
        if(commandExpenseDate < _dateTimeProvider.UtcNow().AddMonths(-3))
            throw new ExpenseDateMoreThanThreeMonthsInThePastException();
    }
}