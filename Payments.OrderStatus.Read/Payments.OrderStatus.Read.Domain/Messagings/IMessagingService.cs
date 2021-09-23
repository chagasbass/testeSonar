using System.Threading.Tasks;

namespace Payments.OrderStatus.Read.Domain.Messagings
{
    public interface IMessagingService
    {
        public string MessageId { get; set; }
        Task<object> ReadMessageQueueAsync();
        Task DeleteMessageQueueAsync();
    }
}
