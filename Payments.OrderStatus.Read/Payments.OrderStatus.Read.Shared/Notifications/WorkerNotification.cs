using Payments.OrderStatus.Read.Shared.Entities;
using Payments.OrderStatus.Read.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Payments.OrderStatus.Read.Shared.Notifications
{
    /// <summary>
    /// Implementação das notificações de domínio
    /// </summary>
    public class WorkerNotification : IWorkerNotification
    {
        private readonly ICollection<Exception> _exceptions;
        private readonly ICollection<Notification> _failures;

        public EStatusCodeOperation StatusCode { get; set; }

        public WorkerNotification()
        {
            _exceptions = new List<Exception>();
            _failures = new List<Notification>();
        }

        public void AddException(Exception exception) => this._exceptions.Add(exception);

        public void AddFailures(IEnumerable<Notification> notifications)
        {
            foreach (var failure in notifications)
                this._failures.Add(failure);
        }

        public void AddFailures(IEnumerable<Notification> notifications, EStatusCodeOperation statusCode)
        {
            AddFailures(notifications);
            AddStatusCode(statusCode);
        }

        public void AddFailure(Notification notification) => _failures.Add(notification);

        public void AddFailure(Notification notification, EStatusCodeOperation statusCode)
            => AddFailure(notification, statusCode);

        public bool HasNotifications() => GetException().Any() || GetFailures().Any();

        public IEnumerable<Exception> GetException() => _exceptions;

        public IEnumerable<Notification> GetFailures() => this._failures;

        public void AddStatusCode(EStatusCodeOperation statusCode) => StatusCode = statusCode;
    }
}
