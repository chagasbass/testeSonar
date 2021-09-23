using System;
using System.Text.Json;

namespace Payments.OrderStatus.Read.Shared.Entities
{
    public class LogData
    {
        public DateTime Timestamp { get; private set; }
        public string ResponseData { get; private set; }
        public int ResponseStatusCode { get; private set; }
        public LogDataExternal LogDataExternal { get; private set; }
        public Exception Exception { get; private set; }

        private bool HasExternalData;

        public LogData()
        {
            Timestamp = DateTime.Now;
        }

        public bool HasExternalInformation() => HasExternalData;

        public LogData SetResponseStatusCode(int statusCode)
        {
            ResponseStatusCode = statusCode;
            return this;
        }

        public LogData SetExternalInformation(LogDataExternal logDataExternal)
        {
            LogDataExternal = new LogDataExternal();
            LogDataExternal = logDataExternal;

            HasExternalData = true;

            return this;
        }

        public LogData SetRequestData(object requestData)
        {
            ResponseData = JsonSerializer.Serialize(requestData);
            return this;
        }

        public LogData SetException(Exception exception)
        {
            Exception = exception;
            return this;
        }
    }
}
