namespace Payments.OrderStatus.Read.Shared.Entities
{
    public class LogDataExternal
    {
        public string RequestBody { get; private set; }
        public string ResponseBody { get; private set; }
        public string Endpoint { get; private set; }
        public int ResponseStatusCode { get; private set; }
        public string Method { get; private set; }

        public LogDataExternal() { }

        public LogDataExternal SetRequestBody(string requestBody)
        {
            RequestBody = requestBody;
            return this;
        }

        public LogDataExternal SetResponseBody(string responseBody)
        {
            ResponseBody = responseBody;
            return this;
        }

        public LogDataExternal SetEndpoint(string endpoint)
        {
            Endpoint = endpoint;
            return this;
        }

        public LogDataExternal SetMethod(string method)
        {
            Method = method;
            return this;
        }


        public LogDataExternal SetResponseStatusCode(int statusCode)
        {
            ResponseStatusCode = statusCode;
            return this;
        }

    }
}
