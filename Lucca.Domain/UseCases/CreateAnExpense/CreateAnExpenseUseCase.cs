using Lucca.Domain.Gateways;
using Lucca.Domain.Models.DateTimeProvider;
using Lucca.Domain.Models.Expenses;
using Lucca.Domain.Models.Expenses.Exceptions;
using Lucca.Domain.Models.IdProvider;
using Lucca.Domain.Models.Users.Exceptions;

namespace Lucca.Domain.UseCases.CreateAnExpense;

public class CreateAnExpenseUseCase
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IIdProvider _idProvider;

    public CreateAnExpenseUseCase(
        IExpenseRepository expenseRepository,
        IUserRepository userRepository,
        IDateTimeProvider dateTimeProvider, 
        IIdProvider idProvider)
    {
        _expenseRepository = expenseRepository;
        _userRepository = userRepository;
        _dateTimeProvider = dateTimeProvider;
        _idProvider = idProvider;
    }
        
    public async Task Handle(CreateAnExpenseCommand command)
    {
        await ThrowIfExpenseAlreadyExists(
            command.UserId, command.ExpenseDate, command.Amount);
        
        var user = await _userRepository.GetBy(command.UserId);
        
        if(user is null)
            throw new UserNotFoundExeption();
        
        await _expenseRepository.Save(Expense.Create
        (
            idProvider: _idProvider,
            dateTimeProvider:_dateTimeProvider,
            user: user,
            expenseDate: command.ExpenseDate,
            type: command.Type,
            amount: command.Amount,
            currency: command.Currency,
            comment: command.Comment
        ));
    }
    

    public async Task ThrowIfExpenseAlreadyExists(
        Guid commandUserId, 
        DateTime commandExpenseDate, 
        decimal commandAmount)
    {
        var expenses = await _expenseRepository.GetBy(
            commandUserId, commandExpenseDate, commandAmount);
        if(expenses.Any())
            throw new ExpenseCannotBeMadeTwiceException();
    }
}