using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Payments.OrderStatus.Read.Shared.Configurations
{
    /// <summary>
    /// Classe de configuração para a troca de parâmetros do worker em tempo de execução
    /// </summary>
    public static class WorkerConfiguration
    {
        public static void ReloadOptions(this WorkerConfigOptions workerConfigOptions)
        {
            var runtimeConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            var value = runtimeConfiguration["WorkerConfig:Runtime"];
            workerConfigOptions.Runtime = Int32.Parse(value);
        }
    }
}
