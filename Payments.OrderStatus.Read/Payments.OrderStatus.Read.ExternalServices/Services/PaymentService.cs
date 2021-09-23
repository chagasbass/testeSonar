using Payments.OrderStatus.Read.Domain.ExternalServices;
using Payments.OrderStatus.Read.ExternalServices.Base;
using Payments.OrderStatus.Read.ExternalServices.Facades;
using Payments.OrderStatus.Read.Shared.Logs;
using Payments.OrderStatus.Read.Shared.Notifications;
using Refit;
using System.Threading.Tasks;

namespace Payments.OrderStatus.Read.ExternalServices.Services
{
    public class PaymentService : ExternalBaseService, IPaymentService
    {
        private readonly IWorkerNotification _notification;
        private readonly IPaymentServiceFacade _paymentServiceFacade;
        private readonly IRequestParamsExternalService _requestParamsExternalService;

        public PaymentService(IWorkerNotification notification,
                              ILogService logService,
                              IRequestParamsExternalService requestParamsExternalService,
                              IPaymentServiceFacade paymentServiceFacade) : base(notification, logService, requestParamsExternalService)
        {
            _notification = notification;
            _requestParamsExternalService = requestParamsExternalService;
            _paymentServiceFacade = paymentServiceFacade;
        }

        public async Task<object> UpdateOrderStatusAsync(object orderStatus)
        {
            _requestParamsExternalService.SetRequestBody(orderStatus);

            ApiResponse<object> orderStatusResponse = await _paymentServiceFacade.UpdateOrderStatusAsync(orderStatus);

            var orderStatusResult = await HandleResponseAsync(orderStatusResponse);

            if (_notification.HasNotifications())
                return default;

            return orderStatusResult;
        }
    }
}
