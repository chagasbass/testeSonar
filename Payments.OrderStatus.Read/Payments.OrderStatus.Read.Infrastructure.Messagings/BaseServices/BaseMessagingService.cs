using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;
using Payments.OrderStatus.Read.Shared.Configurations;

namespace Payments.OrderStatus.Read.Infrastructure.Messagings.BaseServices
{
    /// <summary>
    /// Classe base para serviço de mensagens contendo a criação e o dispose da conexão.
    /// </summary>
    public abstract class BaseMessagingService
    {
        private readonly SnSBaseConfigOptions _sqsBaseConfigOptions;
        public AmazonSQSClient AmazonSQSClient { get; private set; }

        protected BaseMessagingService(IOptionsMonitor<SnSBaseConfigOptions> options)
        {
            _sqsBaseConfigOptions = options.CurrentValue;

            CreateConnection();
        }

        public void CreateConnection()
        {
            var amazonSQSConfig = new AmazonSQSConfig { ServiceURL = _sqsBaseConfigOptions.SqsUrl };
            AmazonSQSClient = new AmazonSQSClient(amazonSQSConfig);
        }

        public void DisposeConnection() => AmazonSQSClient.Dispose();

        public ReceiveMessageRequest CreateReceiveMessageRequest()
        {
            return new ReceiveMessageRequest
            {
                QueueUrl = _sqsBaseConfigOptions.SqsUrl,
                MaxNumberOfMessages = _sqsBaseConfigOptions.MaxNumberOfMessages,
                WaitTimeSeconds = _sqsBaseConfigOptions.WaitTimeSeconds
            };
        }
    }
}
