using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapPost("/transactions", (Transaction transaction) =>
{
    Console.WriteLine($"Received transaction: {transaction}");
    return Results.Ok(transaction);
})
.WithName("CreateTransaction");

app.Run();

public record Transaction(decimal Amount, DateTime When, string Where, string Who, string Shop);