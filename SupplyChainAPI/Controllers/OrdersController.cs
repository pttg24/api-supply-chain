namespace SupplyChainAPI.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly IDeliveriesService _deliveriesService;

        public OrdersController(IOrdersService ordersService, IDeliveriesService deliveriesService)
        {
            _ordersService = ordersService;
            _deliveriesService = deliveriesService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProcessedOrders>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByBrand(string brand)
        {
            var receivedOrders = await _ordersService.GetOrders();
            var orders = _ordersService.FilterAndMapByBrand(receivedOrders.Value, brand);

            var receivedDeliveries = await _deliveriesService.GetDeliveries();
            var processedOrders = _deliveriesService.GetProcessedOrdersByBrand(receivedDeliveries.Value, orders);
            return Ok(processedOrders);
        }
    }
}