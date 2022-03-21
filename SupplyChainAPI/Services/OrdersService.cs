namespace SupplyChainAPI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Domain;
    using Handlers;

    public class OrdersService : IOrdersService
    {
        private readonly IOrdersHandler _ordersHandler;

        public OrdersService(IOrdersHandler ordersHandler)
        {
            _ordersHandler = ordersHandler;
        }

        public async Task<Result<OrderResponse>> GetOrders() =>
            await (await _ordersHandler.RetrieveOrders())
                .OnFailure(e => Result.Failure<OrderResponse>(e))
                .Bind(async r => Result.Success(r));

        public IEnumerable<Order> FilterAndMapByBrand(OrderResponse record, string brand)
        {
            return record.Data.Where(x => 
                x.BrandId == Convert.ToInt32(brand)).Select(
                i => new Order(
                    id: i.Id,
                    brandId: i.BrandId,
                    customerName: i.CustomerName,
                    reference: i.Reference,
                    orderDate: i.OrderDate,
                    priceTotal: i.PriceTotal
                )).ToList();
        }

        public IEnumerable<Order> FilterAndMapByIdOrReference(OrderResponse record, int? id, string? reference)
        {
            return record.Data.Where(x =>
                x.Id == id || x.Reference == reference).Select(
                i => new Order(
                    id: i.Id,
                    brandId: i.BrandId,
                    customerName: i.CustomerName,
                    reference: i.Reference,
                    orderDate: i.OrderDate,
                    priceTotal: i.PriceTotal
                )).ToList();
        }
    }
}