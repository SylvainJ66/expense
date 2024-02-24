using Lucca.Domain.Gateways;
using Lucca.Domain.Models.DateTimeProvider;
using Lucca.Domain.Models.Expenses;
using Lucca.Domain.Models.Expenses.Exceptions;
using Lucca.Domain.Models.Users.Exceptions;

namespace Lucca.Domain.UseCases.CreateAnExpense;

public class CreateAnExpenseUseCase
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateAnExpenseUseCase(
        IExpenseRepository expenseRepository,
        IUserRepository userRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _expenseRepository = expenseRepository;
        _userRepository = userRepository;
        _dateTimeProvider = dateTimeProvider;
    }
        
    public async Task Handle(CreateAnExpenseCommand command)
    {
        await ThrowIfExpenseAlreadyExists(
            command.UserId, command.ExpenseDate, command.Amount);
        
        await ThrowIfUserHasDifferentCurrency(
            command.UserId, command.Currency);
        
        await _expenseRepository.Save(Expense.Create
        (
            dateTimeProvider:_dateTimeProvider,
            userId: command.UserId,
            expenseDate: command.ExpenseDate,
            type: command.Type,
            amount: command.Amount,
            currency: command.Currency,
            comment: command.Comment
        ));
    }

    private async Task ThrowIfUserHasDifferentCurrency(
        string commandUserId, 
        string commandCurrency)
    {
        var user = await _userRepository.GetBy(commandUserId);
        
        if(user is null)
            throw new UserNotFoundExeption();
        
        if(user.Currency != commandCurrency)
            throw new ExpenseAndUserCannotHaveDifferentCurrency();
    }

    private async Task ThrowIfExpenseAlreadyExists(
        string commandUserId, 
        DateTime commandExpenseDate, 
        decimal commandAmount)
    {
        var expenses = await _expenseRepository.GetBy(
            commandUserId, commandExpenseDate, commandAmount);
        if(expenses.Any())
            throw new ExpenseCannotBeMadeTwiceException();
    }
}