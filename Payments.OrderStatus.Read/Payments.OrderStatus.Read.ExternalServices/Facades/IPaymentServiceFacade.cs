using Refit;
using System.Threading.Tasks;

namespace Payments.OrderStatus.Read.ExternalServices.Facades
{
    public interface IPaymentServiceFacade
    {
        [Post("")]
        Task<ApiResponse<object>> UpdateOrderStatusAsync(object orderStatus);
    }
}
