using Payments.OrderStatus.Read.Shared.Entities;
using Payments.OrderStatus.Read.Shared.Logs;
using Serilog;
using System;

namespace Payments.OrderStatus.Read.Infrastructure.Extensions.Logs
{
    public class LogService : ILogService
    {
        public LogData LogData { get; set; }

        private readonly ILogger _logger = Log.ForContext<LogService>();

        public LogService()
        {
            LogData = new LogData();
        }

        public void WriteLog()
        {
            _logger.Information($"[TimeStamp]:{ LogData.Timestamp}");
            _logger.Information($"[Read From Queue]:{ LogData.ResponseData}");

            if (LogData.HasExternalInformation())
            {
                _logger.Information($"[MS Payment Endpoint]:{ LogData.LogDataExternal.Endpoint}");
                _logger.Information($"[MS Payment Method]:{ LogData.LogDataExternal.Method}");
                _logger.Information($"[MS Payment Request]:{ LogData.LogDataExternal.RequestBody}");
                _logger.Information($"[MS Payment ResponseBody]:{ LogData.LogDataExternal.ResponseBody}");
                _logger.Information($"[MS Payment ResponseStatusCode]:{ LogData.LogDataExternal.ResponseStatusCode}");
            }
        }

        public void WriteLogWhenRaiseException()
        {
            _logger.Error($"[Exception]: {LogData.Exception.GetType().Name}");
            _logger.Error($"[Message]: { LogData.Exception.Message}");
            _logger.Error($"[ExceptionStackTrace]: { LogData.Exception.StackTrace}");
            _logger.Error($"[InnerException]: {LogData.Exception?.InnerException?.Message}");
            _logger.Error($"[Stack Chamadas]: { Environment.StackTrace}");
        }

        public void CreateLogData(LogData logData) => LogData = logData;
    }
}
