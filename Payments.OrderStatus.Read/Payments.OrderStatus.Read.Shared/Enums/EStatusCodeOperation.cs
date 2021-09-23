namespace Payments.OrderStatus.Read.Shared.Enums
{
    public class EStatusCodeOperation : Enumeration
    {
        public static EStatusCodeOperation BusinessError { get; } = new EStatusCodeOperation(1, nameof(BusinessError));
        public static EStatusCodeOperation BadRequest { get; } = new EStatusCodeOperation(1, nameof(BadRequest));
        public static EStatusCodeOperation NotFound { get; } = new EStatusCodeOperation(1, nameof(NotFound));
        public static EStatusCodeOperation Post { get; } = new EStatusCodeOperation(1, nameof(Post));
        public static EStatusCodeOperation Get { get; } = new EStatusCodeOperation(1, nameof(Get));
        public static EStatusCodeOperation Put { get; } = new EStatusCodeOperation(1, nameof(Put));
        public static EStatusCodeOperation Patch { get; } = new EStatusCodeOperation(1, nameof(Patch));
        public static EStatusCodeOperation Delete { get; } = new EStatusCodeOperation(1, nameof(Delete));

        public EStatusCodeOperation(int id, string name) : base(id, name) { }
    }
}
