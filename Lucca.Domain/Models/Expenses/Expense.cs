using Lucca.Domain.Models.DateTimeProvider;
using Lucca.Domain.Models.Expenses.Exceptions;

namespace Lucca.Domain.Models.Expenses;

public class Expense
{
    public string UserId { get; }
    public DateTime ExpenseDate { get; }
    public DateTime CreatedAt { get; }
    public ExpenseType Type { get; }
    public decimal Amount { get; }
    public string Currency { get; }
    public string Comment { get; }
    
    private Expense(
        string userId, 
        DateTime expenseDate,
        DateTime createdAt, 
        ExpenseType type, 
        decimal amount, 
        string currency, 
        string comment)
    {
        UserId = userId;
        ExpenseDate = expenseDate;
        CreatedAt = createdAt;
        Type = type;
        Amount = amount;
        Currency = currency;
        Comment = comment;
    }

    public static Expense Create(
        IDateTimeProvider dateTimeProvider,
        string userId, 
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