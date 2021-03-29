using ElasticSearch.API.Business.EntitySearchService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ElasticSearch.API.BackgroundServices
{
    public class CreateIndexHostService : IHostedService
    {
        private const int StartTimeSeconds = 10;

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CreateIndexHostService> _logger;

        public CreateIndexHostService(
            IServiceProvider serviceProvider,
            ILogger<CreateIndexHostService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating index running");

            await Task.Delay(TimeSpan.FromSeconds(StartTimeSeconds), cancellationToken)
               .ContinueWith(CreateIndex, cancellationToken);

            _logger.LogInformation("Creating index finished");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async void CreateIndex(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var entitySearchService = scope.ServiceProvider.GetRequiredService<IEntitySearchService>();

                    await entitySearchService.Index();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error when creating index");
                }
            }
        }
    }
}
