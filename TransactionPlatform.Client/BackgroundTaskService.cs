using System.Collections.Concurrent;

namespace TransactionPlatform.Client
{
    public class BackgroundTaskService : IHostedService
    {
        private readonly HttpClient _client;
        private readonly string _apiUrl = "/transactions";
        private readonly int _numberOfRequests = 4000;
        private readonly int _numberOfIterations = 100;
        private readonly List<Task> _tasks = new List<Task>();
        private Timer _timer;

        public ConcurrentBag<Transaction> Transactions { get; } = new ConcurrentBag<Transaction>();

        public BackgroundTaskService(HttpClient client)
        {
            _client = client;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Run the background task every 5 seconds
            while (true)
            {
                SendRequestsAsync().Wait();
            }
        }

        private void DoWork(object state)
        {
            //for (int iteration = 1; iteration <= _numberOfIterations; iteration++)
            //{
            //    Console.WriteLine($"Iteration {iteration} started...");
            //    SendRequestsAsync().Wait();
            //    Console.WriteLine($"Iteration {iteration} completed.");
            //}

            while (true)
            {
                SendRequestsAsync().Wait();
            }

            Console.WriteLine("All iterations completed.");
        }

        private async Task SendRequestsAsync()
        {
            _tasks.Clear();
            var random = new Random();

            // Create 4000 tasks to send POST requests in parallel
            for (int i = 0; i < random.Next(1000, _numberOfRequests); i++)
            {
                var transaction = new Transaction
                {
                    Amount = random.Next(1, 1000),
                    When = DateTime.UtcNow,
                    Where = $"Location {i}",
                    Who = $"Person {i}",
                    Shop = $"Shop {i}"
                };

                _tasks.Add(SendPostRequestAsync(transaction));
            }

            // Wait for all tasks to complete
            await Task.WhenAll(_tasks);
        }

        private async Task SendPostRequestAsync(Transaction transaction)
        {
            try
            {
                // Send POST request
                HttpResponseMessage response = await _client.PostAsJsonAsync(_apiUrl, transaction);

                // Log response (optional)
                //if (response.IsSuccessStatusCode)
                //{
                //    Console.WriteLine($"Transaction posted successfully: {transaction}");
                //}
                //else
                //{
                //    Console.WriteLine($"Failed to post transaction: {transaction}");
                //}
            }
            catch (Exception ex)
            {
                // Handle any errors
                Console.WriteLine($"Request failed: {ex.Message}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
