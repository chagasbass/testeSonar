using Microsoft.Extensions.Options;
using Payments.OrderStatus.Read.Infrastructure.Messagings.BaseServices;
using Payments.OrderStatus.Read.Shared.Configurations;

namespace Payments.OrderStatus.Read.Tests.Domain.Dummies.BaseMessagingServices
{
    public class BaseMessagingServiceDummie : BaseMessagingService
    {
        private readonly SnSBaseConfigOptions _sqsBaseConfigOptions;

        public BaseMessagingServiceDummie(IOptionsMonitor<SnSBaseConfigOptions> options) : base(options)
        {
            _sqsBaseConfigOptions = options.CurrentValue;
        }
    }
}
