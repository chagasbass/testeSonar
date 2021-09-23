using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payments.OrderStatus.Read.Shared.Configurations;

namespace Payments.OrderStatus.Read.Infrastructure.Extensions.OptionsPattern
{
    /// <summary>
    /// Extension para OptionsPattern
    /// </summary>
    public static class OptionsExtensions
    {
        public static IServiceCollection AddOptionsPattern(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BaseConfigOptions>(configuration.GetSection(BaseConfigOptions.BaseConfig));
            services.Configure<WorkerConfigOptions>(configuration.GetSection(WorkerConfigOptions.BaseConfig));
            services.Configure<ResiliencePoliciesOptions>(configuration.GetSection(ResiliencePoliciesOptions.BaseConfig));
            services.Configure<SnSBaseConfigOptions>(configuration.GetSection(SnSBaseConfigOptions.BaseConfig));
            return services;
        }
    }
}
