var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.TransactionPlatform_ApiService>("apiservice");

builder.AddProject<Projects.TransactionPlatform_Client>("transactionplatform-client")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
