using AutoFixture;
using Payments.OrderStatus.Read.Domain.Entities;
using Payments.OrderStatus.Read.Tests.Bases.Dummies;
using System;

namespace Payments.OrderStatus.Read.Tests.Domain.Dummies.Entities
{
    public class OriginDummie : IDummie<Origin>
    {
        private readonly Fixture _fixture;

        public OriginDummie(Fixture fixture)
        {
            _fixture = fixture;
        }

        public Origin CreateInvalidEntity()
        {
            throw new NotImplementedException();
        }

        public Origin CreateValidEntity()
        {
            throw new NotImplementedException();
        }
    }
}
