using Polly.Retry;

namespace Payments.OrderStatus.Read.Shared.Resiliences
{
    /// <summary>
    /// Contrato para Implementação de Retry Pattern. Caso precise de outro pattern ,criar o contrato aqui
    /// </summary>
    public interface IResilienceService
    {
        AsyncRetryPolicy InsertRetryPolicy();
    }
}
