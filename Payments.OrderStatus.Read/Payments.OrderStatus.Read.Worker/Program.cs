using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Payments.OrderStatus.Read.ExternalServices.Extensions;
using Payments.OrderStatus.Read.Infrastructure.Extensions.DependencyInjections;
using Payments.OrderStatus.Read.Infrastructure.Extensions.Logs;
using Payments.OrderStatus.Read.Infrastructure.Extensions.OptionsPattern;
using Serilog;
using System;
using System.IO;

namespace Payments.OrderStatus.Read.Worker
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = LogExtensions.CreateLogger();

            try
            {
                Log.Information("Iniciando o Worker.");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminado inexperadamente.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IConfiguration GetConfiguration(string[] args, IHostEnvironment environment)
        {
            return new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
               .AddUserSecrets<Program>(optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .AddCommandLine(args)
               .Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    #region configurações do Worker
                    var env = hostContext.HostingEnvironment;

                    var config = GetConfiguration(args, env);

                    services.AddDependencyInjection()
                            .AddOptionsPattern(config)
                            .AddExternalServicesConfigurationIoC(config);

                    services.Configure<HostOptions>(config.GetSection("HostOptions"));

                    services.AddHostedService<OrderStatusWorker>();

                    #endregion
                })
                .UseSerilog();

    }
}
