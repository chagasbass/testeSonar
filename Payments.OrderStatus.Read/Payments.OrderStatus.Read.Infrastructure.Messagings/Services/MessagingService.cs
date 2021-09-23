using Microsoft.Extensions.Options;
using Payments.OrderStatus.Read.Domain.Messagings;
using Payments.OrderStatus.Read.Infrastructure.Messagings.BaseServices;
using Payments.OrderStatus.Read.Shared.Configurations;
using Payments.OrderStatus.Read.Shared.Logs;
using Payments.OrderStatus.Read.Shared.Resiliences;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Payments.OrderStatus.Read.Infrastructure.Messagings.Services
{
    public class MessagingService : BaseMessagingService, IMessagingService
    {
        public string MessageId { get; set; }

        private readonly ILogService _logService;
        private readonly IResilienceService _resilienceService;
        private readonly SnSBaseConfigOptions _sqsBaseConfigOptions;

        public MessagingService(ILogService logService,
                                IOptionsMonitor<SnSBaseConfigOptions> options,
                                IResilienceService resilienceService) : base(options)
        {
            _logService = logService;
            _sqsBaseConfigOptions = options.CurrentValue;
            _resilienceService = resilienceService;
        }

        public async Task DeleteMessageQueueAsync()
        {
            await AmazonSQSClient.DeleteMessageAsync(_sqsBaseConfigOptions.SqsUrl, MessageId);

            DisposeConnection();
        }

        public async Task<object> ReadMessageQueueAsync()
        {
            var receiveMessageRequest = CreateReceiveMessageRequest();

            var resilienceConfiguration = _resilienceService.InsertRetryPolicy();

            await resilienceConfiguration.ExecuteAsync(async () =>
            {
                var receiveMessageResponse = await AmazonSQSClient.ReceiveMessageAsync(receiveMessageRequest);

                if (receiveMessageResponse.HttpStatusCode == HttpStatusCode.OK)
                {
                    var message = receiveMessageResponse.Messages.FirstOrDefault();

                    MessageId = message.MessageId;

                    var order = JsonSerializer.Deserialize<object>(message.Body);

                    _logService.LogData.SetRequestData(order)
                                       .SetResponseStatusCode((int)receiveMessageResponse.HttpStatusCode);

                    return order;
                }

                _logService.LogData.SetResponseStatusCode((int)receiveMessageResponse.HttpStatusCode);

                return default;
            });

            return default;
        }
    }
}
