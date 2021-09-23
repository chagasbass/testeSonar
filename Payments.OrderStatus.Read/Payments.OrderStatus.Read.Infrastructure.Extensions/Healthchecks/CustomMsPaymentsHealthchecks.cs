using Microsoft.Extensions.Diagnostics.HealthChecks;
using Payments.OrderStatus.Read.Shared.Configurations;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Payments.OrderStatus.Read.Infrastructure.Extensions.Healthchecks
{
    public class CustomMsPaymentsHealthchecks : IHealthCheck
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly BaseConfigOptions _baseConfig;


        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = _clientFactory.CreateClient();
                var request = new HttpRequestMessage(HttpMethod.Get, _baseConfig.MsPaymentBaseUrl);
                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return HealthCheckResult.Unhealthy();
            }
            catch
            {
                return HealthCheckResult.Unhealthy();
            }

            return HealthCheckResult.Healthy();
        }
    }
}
