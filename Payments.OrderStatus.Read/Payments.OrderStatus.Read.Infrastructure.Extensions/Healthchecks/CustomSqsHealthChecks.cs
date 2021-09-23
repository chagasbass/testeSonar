using Amazon.SQS;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Payments.OrderStatus.Read.Shared.Configurations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Payments.OrderStatus.Read.Infrastructure.Extensions.Healthchecks
{
    public class CustomSnsHealthChecks : IHealthCheck
    {
        private readonly AmazonSQSClient _queueClient;
        private readonly BaseConfigOptions _baseConfigOptions;

        public CustomSnsHealthChecks(IOptionsMonitor<BaseConfigOptions> options)
        {
            _queueClient = new AmazonSQSClient(Amazon.RegionEndpoint.APSouth1);
            _baseConfigOptions = options.CurrentValue;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var queueUrlResponse = await _queueClient.GetQueueUrlAsync(_baseConfigOptions.SqsQueueName, cancellationToken);

                if (queueUrlResponse.HttpStatusCode == HttpStatusCode.OK)
                    return HealthCheckResult.Healthy();

                return HealthCheckResult.Unhealthy();
            }
            catch
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}
