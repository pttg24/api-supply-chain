namespace SupplyChainAPI.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Domain;

    public interface IOrdersService
    {
        Task<Result<OrderResponse>> GetOrders();

        IEnumerable<Order> FilterAndMapByBrand(OrderResponse record, string brand);

        IEnumerable<Order> FilterAndMapByIdOrReference(OrderResponse record, int? id, string? reference);
    }
}
