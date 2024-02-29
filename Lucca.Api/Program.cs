using Hellang.Middleware.ProblemDetails;
using Lucca.Domain.Gateways;
using Lucca.Domain.Models.DateTimeProvider;
using Lucca.Domain.Models.IdProvider;
using Lucca.Domain.UseCases.CreateAnExpense;
using Lucca.Infrastructure.Sql.Contexts;
using Lucca.Infrastructure.Sql.Expenses;
using Lucca.Infrastructure.Sql.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Hellang.Middleware.ProblemDetails.ProblemDetailsExtensions.AddProblemDetails(builder.Services, c =>
{
    ProblemDetailsConfiguration.Configure(c);
});


//PROVIDERS
builder.Services.AddScoped<IDateTimeProvider, RealDateProvider>();
builder.Services.AddScoped<IIdProvider, RealIdProvider>();

//INFRASTRUCTURE
builder.Services.AddScoped<IExpenseRepository, SqlExpenseRepository>();
builder.Services.AddScoped<IUserRepository, SqlUserRepository>();

builder.Services.AddDbContext<ExpenseDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ExpenseDbContext>();

//USECASES
builder.Services.AddScoped<CreateAnExpenseUseCase>();

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseProblemDetails();

app.Run();

public static class ProblemDetailsConfiguration
{
    public static void Configure(Hellang.Middleware.ProblemDetails.ProblemDetailsOptions options)
    {
        options.GetTraceId = (Func<HttpContext, string>) (ctx => ctx.TraceIdentifier);
        options.OnBeforeWriteDetails = (Action<HttpContext, Microsoft.AspNetCore.Mvc.ProblemDetails>) ((ctx, details) => details.Instance = ctx.Request.Path + ctx.Request.QueryString);
        options.ValidationProblemStatusCode = 400;
        options.MapToStatusCode<NotImplementedException>(501);
        options.MapToStatusCode<HttpRequestException>(503);
        options.MapToStatusCode<Exception>(500);
    }
}