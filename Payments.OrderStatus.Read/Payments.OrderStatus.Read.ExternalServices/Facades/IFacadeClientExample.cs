using Refit;
using System.Threading.Tasks;

namespace Payments.OrderStatus.Read.ExternalServices.Facades
{
    /// <summary>
    /// Interface de exemplo para criação de Facade
    /// </summary>
    public interface IFacadeClientExample
    {
        [Post("/myUrl")]
        Task<ApiResponse<object>> PostObjectAsync(object myObject);
    }
}
