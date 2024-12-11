var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.TransactionPlatform_ApiService>("apiservice")
    .WithReplicas(4);

builder.AddProject<Projects.TransactionPlatform_Client>("transactionplatform-client")
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithReplicas(5);

builder.Build().Run();
