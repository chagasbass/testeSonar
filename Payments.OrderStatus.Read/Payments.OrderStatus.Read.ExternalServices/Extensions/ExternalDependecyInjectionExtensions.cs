using CeA.ResilientHttpClient.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Payments.OrderStatus.Read.ExternalServices.Facades;
using Payments.OrderStatus.Read.Shared.Configurations;
using System;

namespace Payments.OrderStatus.Read.ExternalServices.Extensions
{
    public static class ExternalDependecyInjectionExtensions
    {
        public static IServiceCollection AddExternalServicesConfigurationIoC(this IServiceCollection services,
          IConfiguration configuration)
        {
            #region creating clients

            //Example
            services.AddResilientRefitClient<IPaymentServiceFacade>(configuration)
                .ConfigureHttpClient((sp, client) =>
                {
                    var cfg = sp.GetRequiredService<IOptions<BaseConfigOptions>>().Value;
                    client.BaseAddress = new Uri(cfg.MsPaymentBaseUrl);
                });

            #endregion

            return services;
        }
    }
}
