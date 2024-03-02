using Lucca.ReadSide.Core.Gateways.Queries;
using Lucca.ReadSide.Core.UseCases;
using Lucca.ReadSide.Infrastructure.Sql.Expenses;
using Lucca.Shared.Ressources.Bdd.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//INFRASTRUCTURE
builder.Services.AddScoped<IExpensesHistoryQuery, SqlExpensesHistoryQuery>();
builder.Services.AddDbContext<ExpenseDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ExpenseDbContext>();

//USECASES
builder.Services.AddScoped<GetExpensesByUserUseCase>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
