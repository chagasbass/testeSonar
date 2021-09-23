using Microsoft.Extensions.DependencyInjection;

namespace Payments.OrderStatus.Read.Infrastructure.Extensions.Healthchecks
{
    /// <summary>
    /// Extensao para healthchecks customizados
    /// </summary>
    public static class HealthcheckExtensions
    {
        public static IServiceCollection AddCustomHealthchecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
                  .AddCheck<CustomMsPaymentsHealthchecks>("MS Payments")
                  .AddCheck<CustomSnsHealthChecks>("AWS SQS");

            return services;
        }
    }
}
