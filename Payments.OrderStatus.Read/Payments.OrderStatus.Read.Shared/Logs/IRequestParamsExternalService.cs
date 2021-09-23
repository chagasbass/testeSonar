using System.Collections.Generic;

namespace Payments.OrderStatus.Read.Shared.Logs
{
    public interface IRequestParamsExternalService
    {
        public object RequestBody { get; set; }
        public string QueryParam { get; set; }
        public List<string> QueryParams { get; set; }

        public void SetRequestBody(object requestBody);
        public string GetSerializedRequestBody();
        public void SetRequestQueryParam(string queryParam);
        public void SetRequestQueryParam(List<string> queryParams);
        public string SerializeQueryParams();
    }
}
