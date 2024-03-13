using FluentAssertions;
using Lucca.Domain.Models.DateTimeProvider;
using Lucca.Domain.Models.Expenses;
using Lucca.Domain.Models.Expenses.Exceptions;
using Lucca.Domain.Models.IdProvider;
using Lucca.Domain.Models.Users;
using Lucca.Domain.UseCases.CreateAnExpense;

namespace Lucca.Domain.Tests;

public class CreateAnExpenseUseCaseShould
{
    private readonly InMemoryExpenseRepositoryStub _expenseRepository = new();
    private readonly InMemoryUserRepositoryStub _userRepository = new();
    private readonly DeterministicDateTimeProvider _dateTimeProvider = new();
    private readonly DeterministicIdProvider _idProvider = new();
    private readonly User _user;

    public CreateAnExpenseUseCaseShould()
    {
        _dateTimeProvider.DateOfNow = new DateTime(2024, 1, 2, 14, 15, 3);
        _idProvider.Id = Guid.Parse("c9e0f0d8-25e7-4224-8494-a802a7ac4f3e");
        _user = User.Create(
            idProvider: _idProvider,
            firstname: "John", 
            name: "Doh", 
            currency: "EUR");
        _userRepository.FeedWith(_user);
    }
    
    [Fact]
    public async Task CreateAnExpense()
    {
        var command = new CreateAnExpenseCommand
        (
            UserId: _user.Id,
            Type: ExpenseType.Restaurant,
            Amount: 100,
            ExpenseDate: new DateTime(2024, 1, 1, 00, 00, 00),
            Currency: "EUR",
            Comment: "Lunch"
        );
        
        await CreateAnExpenseUseCase(command);
        
        _expenseRepository.Expenses.Should().BeEquivalentTo(new List<Expense>
        {
            Expense.Create
            (
                idProvider: _idProvider,
                dateTimeProvider: _dateTimeProvider,
                user: _user,
                expenseDate: new DateTime(2024, 1, 1, 00, 00, 00),
                type: ExpenseType.Restaurant,
                amount: 100,
                currency: "EUR",
                comment: "Lunch"
            )
        });
    }

    [Fact]
    public async Task Avoid_date_in_the_future()
    {
        var command = new CreateAnExpenseCommand
        (
            UserId: _user.Id,
            ExpenseDate: new DateTime(2025, 1, 2, 00, 00, 00),
            Type: ExpenseType.Restaurant,
            Amount: 100,
            Currency: "EUR",
            Comment: "Lunch"
        );
        
        var action = () => CreateAnExpenseUseCase(command);
        
        await action.Should().ThrowExactlyAsync<ExpenseDateInTheFutureException>()
            .WithMessage(" : Expense date cannot be in the future");
    }

    [Fact]
    public async Task Avoid_date_more_three_month_in_the_past()
    {
        var command = new CreateAnExpenseCommand
        (
            UserId: _user.Id,
            ExpenseDate: new DateTime(2023, 10, 2, 10, 15, 3),
            Type: ExpenseType.Restaurant,
            Amount: 100,
            Currency: "EUR",
            Comment: "Lunch"
        );
        
        var action = () => CreateAnExpenseUseCase(command);
        
        await action.Should().ThrowExactlyAsync<ExpenseDateMoreThanThreeMonthsInThePastException>()
            .WithMessage(" : Expense date cannot be more than three months in the past");
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Avoid_expense_with_empty_comment(string comment)
    {
        var command = new CreateAnExpenseCommand
        (
            UserId: _user.Id,
            Type: ExpenseType.Restaurant,
            Amount: 100,
            ExpenseDate: new DateTime(2024, 1, 1, 00, 00, 00),
            Currency: "EUR",
            Comment: comment
        );
        
        var action = () => CreateAnExpenseUseCase(command);
        
        await action.Should().ThrowExactlyAsync<ExpenseWithNoCommentException>()
            .WithMessage(" : Expense should have a comment");
    }
    
    [Fact]
    public async Task User_cannot_make_same_expense_twice()
    {
        var expenseDate = new DateTime(2024, 1, 1, 00, 00, 00);
        const int amount = 100;
        
        await _expenseRepository.FeedWith(Expense.Create(
            idProvider: _idProvider,
            dateTimeProvider: _dateTimeProvider,
            user: _user,
            type: ExpenseType.Hotel,
            expenseDate: expenseDate,
            amount: amount,
            currency: "EUR",
            comment: "Hotel"
        ));
        
        var command = new CreateAnExpenseCommand
        (
            UserId: _user.Id,
            Type: ExpenseType.Restaurant,
            Amount: amount,
            ExpenseDate: expenseDate,
            Currency: "EUR",
            Comment: "Lunch"
        );
        
        var action = () => CreateAnExpenseUseCase(command);
        
        await action.Should().ThrowExactlyAsync<ExpenseCannotBeMadeTwiceException>()
            .WithMessage(" : Expense cannot be made twice");
    }

    [Fact]
    public async Task Have_same_currency_that_the_user()
    {
        var command = new CreateAnExpenseCommand
        (
            UserId: _user.Id,
            Type: ExpenseType.Restaurant,
            Amount: 100,
            ExpenseDate: new DateTime(2024, 1, 1, 00, 00, 00),
            Currency: "USD",
            Comment: "Lunch"
        );
        
        var action = () => CreateAnExpenseUseCase(command);

        await action.Should().ThrowExactlyAsync<ExpenseAndUserCannotHaveDifferentCurrency>()
            .WithMessage(" : Expense and user cannot have different currency");
    }
    
    private async Task CreateAnExpenseUseCase(CreateAnExpenseCommand command)
    {
        await new CreateAnExpenseUseCase(
                _expenseRepository, _userRepository, _dateTimeProvider, _idProvider)
            .Handle(command);
    }
}