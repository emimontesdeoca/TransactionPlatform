using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TransactionPlatform.Models;

namespace TransactionPlatform.Client
{
    public class BackgroundTaskService : IHostedService
    {
        private readonly HttpClient _client;
        private readonly string _apiUrl = "/transactions";
        private CancellationTokenSource _cts;

        public BackgroundTaskService(HttpClient client)
        {
            _client = client;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            Task.Run(() => SendRequestsAsync(_cts.Token), _cts.Token);
            return Task.CompletedTask;
        }

        private async Task SendRequestsAsync(CancellationToken cancellationToken)
        {
            var random = new Random();

            while (!cancellationToken.IsCancellationRequested)
            {
                var financed = random.Next(2) == 0;

                var transaction = new Transaction
                {
                    Id = random.Next(),
                    CreatedIn = random.Next(1, 10),
                    ShopId = random.Next(1, 15),
                    CreatedAt = DateTime.UtcNow,
                    Items = new List<Item>(),
                    PaymentTypeId = random.Next(0, 1),
                    Financed = financed,
                    FinancedMonths = financed ? random.Next(1, 24) : -1
                };

                var itemsRandom = random.Next(1, 3);

                for (int i = 0; i < itemsRandom; i++)
                {
                    var item = new Item { ItemId = random.Next(1, 15), Amount = random.Next(1, 2) };

                    transaction.Items.Add(item);
                }

                try
                {
                    HttpResponseMessage response = await _client.PostAsJsonAsync(_apiUrl, transaction, cancellationToken);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Request failed: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(random.Next(1, 3)), cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cts?.Cancel();
            return Task.CompletedTask;
        }
    }
}