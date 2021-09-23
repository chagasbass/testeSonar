using AutoFixture;
using Payments.OrderStatus.Read.Domain.Commands;
using Payments.OrderStatus.Read.Tests.Bases.Dummies;
using System;

namespace Payments.OrderStatus.Read.Tests.Domain.Dummies.Commands
{
    public class ReadOrderStatusCommandDummie : IDummie<ReadOrderStatusCommand>
    {
        private readonly Fixture _fixture;

        public ReadOrderStatusCommandDummie(Fixture fixture)
        {
            _fixture = fixture;
        }

        public ReadOrderStatusCommand CreateInvalidEntity()
        {
            throw new NotImplementedException();
        }

        public ReadOrderStatusCommand CreateValidEntity()
        {
            throw new NotImplementedException();
        }
    }
}
