namespace SupplyChainAPI.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Domain;
    using Handlers;

    public class DeliveriesService : IDeliveriesService
    {
        private readonly IDeliveriesHandler _deliveriesHandler;

        public DeliveriesService(IDeliveriesHandler deliveriesHandler)
        {
            _deliveriesHandler = deliveriesHandler;
        }

        public async Task<Result<DeliveriesResponse>> GetDeliveries() =>
            await (await _deliveriesHandler.RetrieveDeliveries())
                .OnFailure(e => Result.Failure<DeliveriesResponse>(e))
                .Bind(async r => Result.Success(r));


        public IEnumerable<ProcessedOrders> GetProcessedOrdersByBrand(DeliveriesResponse record, IEnumerable<Order> orders)
        {
            return orders.Select(o => new ProcessedOrders()
                {
                    Id = o.Id,
                    BrandId = o.BrandId,
                    CustomerName = o.CustomerName,
                    Reference = o.Reference,
                    OrderDate = o.OrderDate,
                    PriceTotal = o.PriceTotal,
                    Deliveries = record.Data.Where(d=> d.OrderId == o.Id).Select(i=> 
                        new Delivery(
                            id: i.Id, 
                            orderId: i.OrderId, 
                            shipped: i.Shipped, 
                            products: i.Products))
                        .ToList()
                })
                .ToList();
        }

        public IEnumerable<Delivery> FilterByShippedOrders(DeliveriesResponse record)
        {
            return record.Data.Where(x =>
                x.Shipped == true).Select(
                i => new Delivery(
                    id: i.Id,
                    orderId: i.OrderId,
                    shipped: i.Shipped,
                    products: i.Products
                )).ToList();
        }

        public IEnumerable<ProcessedOrdersWithProducts> GetShippedQuantities(IEnumerable<Order> orders,
            IEnumerable<Delivery> deliveries)
        {
            return orders.Select(o => new ProcessedOrdersWithProducts()
                {
                    Id = o.Id,
                    BrandId = o.BrandId,
                    CustomerName = o.CustomerName,
                    Reference = o.Reference,
                    OrderDate = o.OrderDate,
                    PriceTotal = o.PriceTotal,
                    Products = deliveries.Where(d => d.OrderId == o.Id).SelectMany(i => i.Products)
                        .Select(d => new Product( 
                            quantity: d.Quantity,
                            productName: d.ProductName
                        )).ToList()
                })
                .ToList();
        }
    }
}
