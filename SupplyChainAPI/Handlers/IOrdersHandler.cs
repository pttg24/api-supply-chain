namespace SupplyChainAPI.Handlers
{
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Domain;

    public interface IOrdersHandler
    {
        Task<Result<OrderResponse>> RetrieveOrders();
    }
}
