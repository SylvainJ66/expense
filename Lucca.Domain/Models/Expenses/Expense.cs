using Lucca.Domain.Models.DateTimeProvider;
using Lucca.Domain.Models.Expenses.Exceptions;
using Lucca.Domain.Models.IdProvider;

namespace Lucca.Domain.Models.Expenses;

public class Expense
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public DateTime ExpenseDate { get; }
    public DateTime CreatedAt { get; }
    public ExpenseType Type { get; }
    public decimal Amount { get; }
    public string Currency { get; }
    public string Comment { get; }
    
    private Expense(
        Guid id,
        Guid userId, 
        DateTime expenseDate,
        DateTime createdAt, 
        ExpenseType type, 
        decimal amount, 
        string currency, 
        string comment)
    {
        Id = id;
        UserId = userId;
        ExpenseDate = expenseDate;
        CreatedAt = createdAt;
        Type = type;
        Amount = amount;
        Currency = currency;
        Comment = comment;
    }

    public static Expense Create(
        IIdProvider idProvider,
        IDateTimeProvider dateTimeProvider,
        Guid userId, 
        DateTime expenseDate,
        ExpenseType type, 
        decimal amount, 
        string currency, 
        string comment)
    {
        ThrowIfDateIsInTheFuture(dateTimeProvider, expenseDate);
        ThrowIfDateIsMoreThanThreeMonthsInThePast(dateTimeProvider, expenseDate);
        ThrowIfNoComment(comment);
        
        return new Expense(
            id: idProvider.NewId(),
            userId, 
            expenseDate, 
            createdAt: dateTimeProvider.UtcNow(), 
            type, 
            amount, 
            currency, 
            comment);
    }
    
    private static void ThrowIfDateIsInTheFuture(IDateTimeProvider dateTimeProvider, DateTime commandExpenseDate)
    {
        if(commandExpenseDate > dateTimeProvider.UtcNow())
            throw new ExpenseDateInTheFutureException();
    }
    
    private static void ThrowIfDateIsMoreThanThreeMonthsInThePast(IDateTimeProvider dateTimeProvider, DateTime commandExpenseDate)
    {
        if(commandExpenseDate < dateTimeProvider.UtcNow().AddMonths(-3))
            throw new ExpenseDateMoreThanThreeMonthsInThePastException();
    }
    
    private static void ThrowIfNoComment(string commandComment)
    {
        if(string.IsNullOrWhiteSpace(commandComment))
            throw new ExpenseWithNoCommentException();
    }
}