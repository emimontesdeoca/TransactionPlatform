using Azure.Messaging.ServiceBus;
using System.Text.Json;
using TransactionPlatform.Models;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
// Assuming builder.AddServiceDefaults() is a custom extension method.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

string serviceBusConnectionString = "";
string queueName = "";

await using ServiceBusClient client = new(serviceBusConnectionString);

// create the sender
ServiceBusSender sender = client.CreateSender(queueName);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapPost("/transactions", async (Transaction transaction) =>
{
    // Serialize the transaction to JSON.
    string transactionJson = JsonSerializer.Serialize(transaction);

    // Create a new ServiceBusMessage.
    ServiceBusMessage message = new ServiceBusMessage(transactionJson);

    // Send the message to the queue.
    await sender.SendMessageAsync(message);

    return Results.Ok(transaction);
})
.WithName("CreateTransaction");

app.Run();
