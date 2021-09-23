namespace Payments.OrderStatus.Read.Shared.Configurations
{
    /// <summary>
    /// Options para configuração da fila onde :
    /// SqsUrl = Url da fila criada
    /// WaitTimeSeconds =tempo de leitura entre as mensagens.default(2)
    /// MaxNumberOfMessages = número de mensagens que serão retiradas da fila para leitura.(default 1)
    /// </summary>
    public class SnSBaseConfigOptions
    {
        public const string BaseConfig = "SQSBaseConfig";
        public string SqsUrl { get; set; }
        public int WaitTimeSeconds { get; set; }
        public int MaxNumberOfMessages { get; set; }

        public SnSBaseConfigOptions() { }

    }
}
