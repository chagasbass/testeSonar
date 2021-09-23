using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Payments.OrderStatus.Read.Domain.ExternalServices;
using Payments.OrderStatus.Read.Domain.Handlers;
using Payments.OrderStatus.Read.Domain.Messagings;
using Payments.OrderStatus.Read.ExternalServices.Services;
using Payments.OrderStatus.Read.Infrastructure.Extensions.Logs;
using Payments.OrderStatus.Read.Infrastructure.Messagings.Services;
using Payments.OrderStatus.Read.Shared.Entities;
using Payments.OrderStatus.Read.Shared.Logs;
using Payments.OrderStatus.Read.Shared.Notifications;
using Payments.OrderStatus.Read.Shared.Resiliences;

namespace Payments.OrderStatus.Read.Infrastructure.Extensions.DependencyInjections
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            #region resilience and notification

            services.AddScoped<IResilienceService, ResilienceService>();
            services.AddScoped<IWorkerNotification, WorkerNotification>();

            #endregion

            #region logs

            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<LogData>();

            #endregion

            #region external services
            services.AddScoped<IRequestParamsExternalService, RequestParamsExternalService>();

            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IMessagingService, MessagingService>();
            #endregion

            #region handlers
            services.AddMediatR(typeof(ReadOrderStatusHandler).Assembly);
            #endregion

            return services;
        }
    }
}
