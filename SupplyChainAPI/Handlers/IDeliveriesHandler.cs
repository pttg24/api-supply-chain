namespace SupplyChainAPI.Handlers
{
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Domain;

    public interface IDeliveriesHandler
    {
        Task<Result<DeliveriesResponse>> RetrieveDeliveries();
    }
}
