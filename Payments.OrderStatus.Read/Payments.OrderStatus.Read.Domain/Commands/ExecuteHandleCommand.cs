using MediatR;

namespace Payments.OrderStatus.Read.Domain.Commands
{
    public class ExecuteHandleCommand : IRequest<bool> { }

}
