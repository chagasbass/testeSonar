using Serilog;
using Serilog.Core;
using Serilog.Enrichers.Span;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Formatting.Compact;

namespace Payments.OrderStatus.Read.Infrastructure.Extensions.Logs
{
    /// <summary>
    /// Extension para configuração do Serilog.
    /// </summary>
    public static class LogExtensions
    {
        public static Logger CreateLogger()
        {
            var levelSwitch = new LoggingLevelSwitch
            {
                MinimumLevel = LogEventLevel.Information
            };

            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.ControlledBy(levelSwitch)
                .Enrich.FromLogContext()
                .WriteTo.Console(new RenderedCompactJsonFormatter(), restrictedToMinimumLevel: LogEventLevel.Information)
                .Enrich.WithExceptionDetails
                (
                    new DestructuringOptionsBuilder()
                    .WithIgnoreStackTraceAndTargetSiteExceptionFilter()
                    .WithDefaultDestructurers()
                )
                .Enrich.WithSpan();

            return logger.CreateLogger();
        }
    }
}
