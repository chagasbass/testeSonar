using System.Threading.Tasks;

namespace Payments.OrderStatus.Read.Domain.ExternalServices
{
    public interface IPaymentService
    {
        Task<object> UpdateOrderStatusAsync(object orderStatus);
    }
}
