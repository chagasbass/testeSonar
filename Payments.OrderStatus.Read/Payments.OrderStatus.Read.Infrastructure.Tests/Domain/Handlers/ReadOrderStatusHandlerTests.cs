using AutoFixture;
using Moq;
using Payments.OrderStatus.Read.Domain.Commands;
using Payments.OrderStatus.Read.Domain.ExternalServices;
using Payments.OrderStatus.Read.Domain.Handlers;
using Payments.OrderStatus.Read.Domain.Messagings;
using Payments.OrderStatus.Read.Shared.Notifications;
using Payments.OrderStatus.Read.Tests.Domain.Dummies.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Payments.OrderStatus.Read.Infrastructure.Tests.Domain.Handlers
{
    public class ReadOrderStatusHandlerTests
    {
        #region Declaracoes

        private readonly Mock<IWorkerNotification> _workerNotification;
        private readonly Mock<IPaymentService> _paymentService;
        private readonly Mock<IMessagingService> _messagingService;
        private readonly Fixture _fixture;
        private readonly ReadOrderStatusCommandDummie _readOrderStatusCommandDummie;

        #endregion

        public ReadOrderStatusHandlerTests()
        {
            _workerNotification = new Mock<IWorkerNotification>();
            _paymentService = new Mock<IPaymentService>();
            _messagingService = new Mock<IMessagingService>();
            _fixture = new Fixture();
            _readOrderStatusCommandDummie = new ReadOrderStatusCommandDummie(_fixture);

        }

        [Fact]
        [Trait("ReadOrderStatusHandler", "Fluxo de sucesso - leitura da fila e update no servico.")]
        public async Task ShouldProcessOrderOnQueueAndUpdateOnServiceWhenThereIsNotNotification()
        {
            var readOrderStatusCommand = _readOrderStatusCommandDummie.CreateValidEntity();

            _messagingService.Setup(ms => ms.ReadMessageQueueAsync()).ReturnsAsync(readOrderStatusCommand);
            _messagingService.Setup(ms => ms.DeleteMessageQueueAsync());
            _paymentService.Setup(ps => ps.UpdateOrderStatusAsync(It.IsAny<object>())).ReturnsAsync(readOrderStatusCommand);

            var readOrderStatusHandler = new ReadOrderStatusHandler(_workerNotification.Object,
                                                                      _paymentService.Object,
                                                                      _messagingService.Object);

            var executeHandleCommand = new ExecuteHandleCommand();
            var cancelationToken = new CancellationToken();

            var isHandleExecutedSafe = await readOrderStatusHandler.Handle(executeHandleCommand, cancelationToken);

            Assert.True(isHandleExecutedSafe);
        }

        [Fact]
        [Trait("ReadOrderStatusHandler", "Fluxo de sucesso - leitura da fila mas sem mensagens para processamento.")]
        public async Task ShouldDoNothingWhenOrderQueueDontContainOrdersForReading()
        {
            var readOrderStatusCommand = _readOrderStatusCommandDummie.CreateValidEntity();

            _messagingService.Setup(ms => ms.ReadMessageQueueAsync()).ReturnsAsync(default);
            _messagingService.Setup(ms => ms.DeleteMessageQueueAsync());
            _paymentService.Setup(ps => ps.UpdateOrderStatusAsync(It.IsAny<object>())).ReturnsAsync(readOrderStatusCommand);

            var readOrderStatusHandler = new ReadOrderStatusHandler(_workerNotification.Object,
                                                                      _paymentService.Object,
                                                                      _messagingService.Object);

            var executeHandleCommand = new ExecuteHandleCommand();
            var cancelationToken = new CancellationToken();

            var isHandleExecutedSafe = await readOrderStatusHandler.Handle(executeHandleCommand, cancelationToken);

            Assert.True(isHandleExecutedSafe);
        }

        [Fact]
        [Trait("ReadOrderStatusHandler", "Fluxo alternativo - leitura da fila mas gera notificação ao atualizar no serviço de pagamentos.")]
        public async Task ShouldCreateNotificationWhenUpdateOrderOnServiceRaiseErrors()
        {
            var readOrderStatusCommand = _readOrderStatusCommandDummie.CreateValidEntity();

            _messagingService.Setup(ms => ms.ReadMessageQueueAsync()).ReturnsAsync(readOrderStatusCommand);
            _messagingService.Setup(ms => ms.DeleteMessageQueueAsync());
            _paymentService.Setup(ps => ps.UpdateOrderStatusAsync(It.IsAny<object>())).ReturnsAsync(null);

            var readOrderStatusHandler = new ReadOrderStatusHandler(_workerNotification.Object,
                                                                      _paymentService.Object,
                                                                      _messagingService.Object);

            var executeHandleCommand = new ExecuteHandleCommand();
            var cancelationToken = new CancellationToken();

            var isHandleExecutedSafe = await readOrderStatusHandler.Handle(executeHandleCommand, cancelationToken);

            Assert.True(_workerNotification.Invocations.Any());
            Assert.False(isHandleExecutedSafe);
        }

        [Fact]
        [Trait("ReadOrderStatusHandler", "Fluxo alternativo - leitura da fila, atualiza no serviço de pagamentos mas gera erro ao excluir mensagem na fila.")]
        public async Task ShouldRaiseAnExceptionWhenDeleteQueueOrdeMessageRaisesErros()
        {
            var readOrderStatusCommand = _readOrderStatusCommandDummie.CreateValidEntity();

            _messagingService.Setup(ms => ms.ReadMessageQueueAsync()).ReturnsAsync(readOrderStatusCommand);
            _messagingService.Setup(ms => ms.DeleteMessageQueueAsync()).ThrowsAsync(new Exception());
            _paymentService.Setup(ps => ps.UpdateOrderStatusAsync(It.IsAny<object>())).ReturnsAsync(readOrderStatusCommand);

            var readOrderStatusHandler = new ReadOrderStatusHandler(_workerNotification.Object,
                                                                      _paymentService.Object,
                                                                      _messagingService.Object);

            var executeHandleCommand = new ExecuteHandleCommand();
            var cancelationToken = new CancellationToken();

            await Assert.ThrowsAsync<Exception>(() => readOrderStatusHandler.Handle(executeHandleCommand, cancelationToken));
        }
    }
}
