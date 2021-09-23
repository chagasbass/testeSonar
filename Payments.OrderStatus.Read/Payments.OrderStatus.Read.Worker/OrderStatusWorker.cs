using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Payments.OrderStatus.Read.Domain.Commands;
using Payments.OrderStatus.Read.Shared.Configurations;
using Payments.OrderStatus.Read.Shared.Logs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payments.OrderStatus.Read.Worker
{
    public class OrderStatusWorker : BackgroundService
    {
        private readonly ILogger<OrderStatusWorker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IMediator _mediator;
        private readonly ILogService _logService;

        private readonly WorkerConfigOptions _workerConfigOptions;

        public OrderStatusWorker(ILogger<OrderStatusWorker> logger,
                                 IServiceScopeFactory serviceScopeFactory,
                                 IHostApplicationLifetime hostApplicationLifetime,
                                 IOptionsMonitor<WorkerConfigOptions> options,
                                 IMediator mediator,
                                 ILogService logService)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _hostApplicationLifetime = hostApplicationLifetime;
            _mediator = mediator;
            _logService = logService;
            _workerConfigOptions = options.CurrentValue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using var scope = _serviceScopeFactory.CreateScope();

                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                    var executeCommand = new ExecuteHandleCommand();

                    await _mediator.Publish(executeCommand, stoppingToken);

                    await Task.Delay(_workerConfigOptions.Runtime, stoppingToken);

                    _workerConfigOptions.ReloadOptions();
                }
            }
            catch (Exception ex)
            {
                _logService.LogData.SetException(ex);
                _logService.WriteLogWhenRaiseException();
            }
            finally
            {
                _hostApplicationLifetime.StopApplication();
            }
        }
    }
}
