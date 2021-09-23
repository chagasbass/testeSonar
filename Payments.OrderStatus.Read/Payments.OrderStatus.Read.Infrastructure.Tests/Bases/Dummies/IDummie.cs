namespace Payments.OrderStatus.Read.Tests.Bases.Dummies
{
    /// <summary>
    /// Interface para criação de Dummies para teste
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDummie<T>
    {
        T CreateValidEntity();
        T CreateInvalidEntity();
    }
}
