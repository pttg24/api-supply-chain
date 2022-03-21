namespace SupplyChainAPI.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Domain;

    public interface IDeliveriesService
    {
        Task<Result<DeliveriesResponse>> GetDeliveries();

        IEnumerable<ProcessedOrders> GetProcessedOrdersByBrand(DeliveriesResponse record, IEnumerable<Order> orders);

        IEnumerable<Delivery> FilterByShippedOrders(DeliveriesResponse record);

        IEnumerable<ProcessedOrdersWithProducts> GetShippedQuantities(IEnumerable<Order> orders, IEnumerable<Delivery> deliveries);
    }
}
