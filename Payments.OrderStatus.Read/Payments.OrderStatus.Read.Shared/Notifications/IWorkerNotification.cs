using Payments.OrderStatus.Read.Shared.Entities;
using Payments.OrderStatus.Read.Shared.Enums;
using System;
using System.Collections.Generic;

namespace Payments.OrderStatus.Read.Shared.Notifications
{
    /// <summary>
    /// Interface de notificação
    /// </summary>
    public interface IWorkerNotification
    {
        EStatusCodeOperation StatusCode { get; set; }
        void AddException(Exception exception);
        void AddFailure(Notification notification);
        void AddFailure(Notification notification, EStatusCodeOperation statusCode);
        void AddFailures(IEnumerable<Notification> notifications);
        void AddFailures(IEnumerable<Notification> notifications, EStatusCodeOperation statusCode);
        void AddStatusCode(EStatusCodeOperation statusCode);
        IEnumerable<Exception> GetException();
        IEnumerable<Notification> GetFailures();
        bool HasNotifications();
    }
}
