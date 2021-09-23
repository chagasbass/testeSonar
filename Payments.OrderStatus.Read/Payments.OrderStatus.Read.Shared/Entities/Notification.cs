using Payments.OrderStatus.Read.Shared.Enums;

namespace Payments.OrderStatus.Read.Shared.Entities
{

    public class Notification
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public EStatusCodeOperation StatusCodeOperation { get; set; }

        public Notification(string propertyName, string errorMessage, EStatusCodeOperation statusCodeOperation)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            StatusCodeOperation = statusCodeOperation;
        }

        public Notification(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        public override string ToString() => $"Property:{PropertyName} - Error: {ErrorMessage}";
    }

}
