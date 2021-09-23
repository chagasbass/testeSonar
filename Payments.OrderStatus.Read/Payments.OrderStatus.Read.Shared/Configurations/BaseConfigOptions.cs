namespace Payments.OrderStatus.Read.Shared.Configurations
{
    public class BaseConfigOptions
    {
        public const string BaseConfig = "BaseConfig";

        public string MsPaymentBaseUrl { get; set; }
        public string SqsUrl { get; set; }
        public string SqsQueueName { get; set; }

        public BaseConfigOptions() { }

    }
}
