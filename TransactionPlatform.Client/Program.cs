using System.Collections.Concurrent;
using TransactionPlatform.Client;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddHostedService(provider => provider.GetRequiredService<BackgroundTaskService>());

builder.Services.AddHttpClient<BackgroundTaskService>(client =>
{
    // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
    // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
    client.BaseAddress = new("https+http://apiservice");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();


app.Run();
