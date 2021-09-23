using Payments.OrderStatus.Read.Shared.Entities;

namespace Payments.OrderStatus.Read.Shared.Logs
{
    public interface ILogService
    {
        public LogData LogData { get; set; }
        void WriteLog();
        void CreateLogData(LogData logData);
        void WriteLogWhenRaiseException();
    }
}
