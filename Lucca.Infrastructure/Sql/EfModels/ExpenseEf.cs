namespace Lucca.Infrastructure.Sql.EfModels;

public class ExpenseEf
{
    public Guid Id { get; set; }
    public DateTime ExpenseDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ExpenseType { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Comment { get; set; }
    public Guid UserId { get; set; }
}