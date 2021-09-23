using Microsoft.Extensions.Options;
using Payments.OrderStatus.Read.Shared.Configurations;
using Polly;
using Polly.Retry;
using System;
using System.Net.Http;

namespace Payments.OrderStatus.Read.Shared.Resiliences
{
    public class ResilienceService : IResilienceService
    {
        private readonly ResiliencePoliciesOptions _resiliencePoliciesOptions;

        public ResilienceService(IOptionsMonitor<ResiliencePoliciesOptions> options)
        {
            _resiliencePoliciesOptions = options.CurrentValue;
        }
        public AsyncRetryPolicy InsertRetryPolicy()
        {
            var retryPolicy = Policy
                       .Handle<HttpRequestException>()
                       .WaitAndRetryAsync(_resiliencePoliciesOptions.RetryTimes,
                                         i => TimeSpan.FromMilliseconds(_resiliencePoliciesOptions.PauseBetweenFailures));

            return retryPolicy;
        }
    }
}
