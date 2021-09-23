using Payments.OrderStatus.Read.Shared.Logs;
using System.Collections.Generic;
using System.Text.Json;

namespace Payments.OrderStatus.Read.Infrastructure.Extensions.Logs
{
    public class RequestParamsExternalService : IRequestParamsExternalService
    {
        public string QueryParam { get; set; }
        public List<string> QueryParams { get; set; }
        public object RequestBody { get; set; }

        public RequestParamsExternalService() { }

        public string GetSerializedRequestBody() => RequestBody.ToString();
        public void SetRequestQueryParam(string queryParam) => QueryParams.Add(queryParam);
        public void SetRequestQueryParam(List<string> queryParams) => QueryParams = queryParams;
        public string SerializeQueryParams() => JsonSerializer.Serialize(QueryParams);
        public void SetRequestBody(object requestBody)
        {
            if (requestBody is not null)
            {
                var requestJson = JsonSerializer.Serialize(requestBody);
                RequestBody = requestJson;
            }
        }
    }
}
