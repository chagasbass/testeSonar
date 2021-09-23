using Payments.OrderStatus.Read.Shared.Entities;
using Payments.OrderStatus.Read.Shared.Logs;
using Payments.OrderStatus.Read.Shared.Notifications;
using Refit;
using System.Text.Json;
using System.Threading.Tasks;

namespace Payments.OrderStatus.Read.ExternalServices.Base
{
    public abstract class ExternalBaseService
    {
        private readonly IWorkerNotification _notification;
        private readonly ILogService _logService;
        private readonly IRequestParamsExternalService _requestParamsExternalService;

        public object Request { get; set; }

        protected ExternalBaseService(IWorkerNotification notification,
                                      ILogService logService,
                                      IRequestParamsExternalService requestParamsExternalService)
        {
            _notification = notification;
            _logService = logService;
            _requestParamsExternalService = requestParamsExternalService;
        }

        internal async Task<T> HandleResponseAsync<T>(ApiResponse<T> response) where T : class
        {
            if (response.Content == null && response.Error == null)
            {
                _notification.AddFailure(new Notification("", "TODO: Tratar esse fluxo na implementação"));

                InstantiateLogOperation(response);

                return default;
            }

            if (response.IsSuccessStatusCode)
            {
                InstantiateLogOperation(response);

                return response.Content;
            }
            else
            {
                InstantiateLogOperation(response);

                return default;
            }
        }

        private void InstantiateLogOperation<T>(ApiResponse<T> response) where T : class
        {
            var responseContent = JsonSerializer.Serialize(response.Content);

            _logService.LogData.SetRequestData(_requestParamsExternalService.GetSerializedRequestBody())
                               .LogDataExternal
                                   .SetMethod(response.RequestMessage.Method.Method)
                                   .SetEndpoint(response.RequestMessage.RequestUri.ToString())
                                   .SetRequestBody(_requestParamsExternalService.GetSerializedRequestBody())
                                   .SetResponseBody(responseContent)
                                   .SetResponseStatusCode((int)response.StatusCode);

            _logService.WriteLog();
        }
    }
}
