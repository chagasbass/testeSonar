namespace Payments.OrderStatus.Read.Shared.Configurations
{
    /// <summary>
    /// Políticas de resiliência para chamadas.
    /// </summary>
    public class ResiliencePoliciesOptions
    {
        public const string BaseConfig = "ResiliencePolicies";
        public int PauseBetweenFailures { get; set; }
        public int RetryTimes { get; set; }

        public ResiliencePoliciesOptions() { }

    }
}
