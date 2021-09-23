using MediatR;
using Payments.OrderStatus.Read.Domain.Commands;
using Payments.OrderStatus.Read.Domain.ExternalServices;
using Payments.OrderStatus.Read.Domain.Messagings;
using Payments.OrderStatus.Read.Shared.Notifications;
using System.Threading;
using System.Threading.Tasks;

namespace Payments.OrderStatus.Read.Domain.Handlers
{
    public class ReadOrderStatusHandler : IRequestHandler<ExecuteHandleCommand, bool>
    {
        private readonly IWorkerNotification _workerNotification;
        private readonly IPaymentService _paymentService;
        private readonly IMessagingService _messagingService;

        public ReadOrderStatusHandler(IWorkerNotification workerNotification,
                                      IPaymentService paymentService,
                                      IMessagingService messagingService)
        {
            _workerNotification = workerNotification;
            _paymentService = paymentService;
            _messagingService = messagingService;
        }

        public async Task<bool> Handle(ExecuteHandleCommand request, CancellationToken cancellationToken)
        {
            var orderstatusQueueMessage = await _messagingService.ReadMessageQueueAsync();

            var isHandleExecutedSafe = true;

            //TODO: entra aqui caso leia a fila e não tenha nada para processamento
            //deve notificar algum código para que no fluxo do worker vá para o tempo de espera novamente
            if (orderstatusQueueMessage is not null)
            {
                await _paymentService.UpdateOrderStatusAsync(orderstatusQueueMessage);

                if (!_workerNotification.HasNotifications())
                {
                    await _messagingService.DeleteMessageQueueAsync();
                    return isHandleExecutedSafe;
                }

                isHandleExecutedSafe = false;
                return isHandleExecutedSafe;
            }

            return isHandleExecutedSafe;
        }
    }
}
