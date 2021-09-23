using Microsoft.Extensions.Options;
using Moq;
using Payments.OrderStatus.Read.Shared.Configurations;
using Payments.OrderStatus.Read.Tests.Domain.Dummies.BaseMessagingServices;
using Xunit;

namespace Payments.OrderStatus.Read.Tests.Domain.Messagings
{
    public class BaseMessagingServiceTests
    {
        private readonly Mock<IOptionsMonitor<SnSBaseConfigOptions>> _optionsMock;
        private readonly SnSBaseConfigOptions _snSBaseConfigOptions;
        private readonly BaseMessagingServiceDummie _baseMessagingServiceDummie;

        public BaseMessagingServiceTests()
        {
            _snSBaseConfigOptions = new SnSBaseConfigOptions
            {
                MaxNumberOfMessages = 1,
                SqsUrl = "",
                WaitTimeSeconds = 5
            };

            _optionsMock = new Mock<IOptionsMonitor<SnSBaseConfigOptions>>();
            _optionsMock.Setup(o => o.CurrentValue).Returns(_snSBaseConfigOptions);

            _baseMessagingServiceDummie = new BaseMessagingServiceDummie(_optionsMock.Object);
        }

        [Fact]
        [Trait("BaseMessagingService", "Criação da conexão com a fila SQS")]
        public void ShouldCreateConnectionWithSnS()
        {
            _baseMessagingServiceDummie.CreateConnection();
            Assert.False(true);
        }

        [Fact]
        [Trait("BaseMessagingService", "Criação da conexão com a fila SQS")]
        public void ShouldRaiseExceptionWhenCreateConnectionWithSnSRaisesError()
        {
            Assert.False(true);
        }

        [Fact]
        [Trait("BaseMessagingService", "Dispose da conexão com a fila SQS")]
        public void ShouldDisposeConnectionWithSnS()
        {
            Assert.False(true);
        }

        [Fact]
        [Trait("BaseMessagingService", "Dispose da conexão com a fila SQS")]
        public void ShouldRaiseExceptionWhenDisposeConnectionWithSnSRaisesError()
        {
            Assert.False(true);
        }

        [Fact]
        [Trait("BaseMessagingService", "Criação do objeto de request para a fila SQS")]
        public void ShouldCreateAndReturnAReceiveMessageRequestObject()
        {
            Assert.False(true);
        }
    }
}
