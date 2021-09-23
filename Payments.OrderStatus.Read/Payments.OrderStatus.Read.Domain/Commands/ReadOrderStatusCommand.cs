using MediatR;
using Payments.OrderStatus.Read.Domain.Entities;

namespace Payments.OrderStatus.Read.Domain.Commands
{
    public class ReadOrderStatusCommand : IRequest<ReadOrderStatusCommand>
    {
        public string Domain { get; set; }
        public string OrderId { get; set; }
        public string State { get; set; }

        public string LastState { get; set; }

        public string LastChange { get; set; }

        public string CurrentChange { get; set; }

        public Origin Origin { get; set; }

        public ReadOrderStatusCommand() { }
    }
}
