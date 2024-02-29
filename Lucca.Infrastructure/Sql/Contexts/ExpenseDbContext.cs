using Lucca.Infrastructure.Sql.EfModels;
using Microsoft.EntityFrameworkCore;

namespace Lucca.Infrastructure.Sql.Contexts;

public class ExpenseDbContext : DbContext
{
    private readonly string? _defaultSchema;
    public DbSet<ExpenseEf> Expenses { get; set; }
    public DbSet<UserEf> Users { get; set; }

    public ExpenseDbContext(DbContextOptions options, string? defaultSchema = null) : base(options)
    {
        _defaultSchema = defaultSchema;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (_defaultSchema != null)
            modelBuilder.HasDefaultSchema(_defaultSchema);
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ExpenseEf>().ToTable("expenses");
        modelBuilder.Entity<ExpenseEf>()
            .Property(e => e.Id)
            .HasColumnName("id");
        modelBuilder.Entity<ExpenseEf>()
            .Property(e => e.UserId)
            .HasColumnName("user_id");
        modelBuilder.Entity<ExpenseEf>()
            .Property(e => e.ExpenseDate)
            .HasColumnName("expense_date")
            .HasColumnType("timestamp");;
        modelBuilder.Entity<ExpenseEf>()
            .Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp");
        modelBuilder.Entity<ExpenseEf>()
            .Property(e => e.ExpenseType)
            .HasColumnName("expense_type");
        modelBuilder.Entity<ExpenseEf>()
            .Property(e => e.Amount)
            .HasColumnName("amount");
        modelBuilder.Entity<ExpenseEf>()
            .Property(e => e.Currency)
            .HasColumnName("currency");
        modelBuilder.Entity<ExpenseEf>()
            .Property(e => e.Comment)
            .HasColumnName("comment");
        
        modelBuilder.Entity<UserEf>().ToTable("users");
        modelBuilder.Entity<UserEf>()
            .Property(e => e.Id)
            .HasColumnName("id");
        modelBuilder.Entity<UserEf>()
            .Property(e => e.Firstname)
            .HasColumnName("first_name");
        modelBuilder.Entity<UserEf>()
            .Property(e => e.Name)
            .HasColumnName("name");
        modelBuilder.Entity<UserEf>()
            .Property(e => e.Currency)
            .HasColumnName("currency");
    }
}