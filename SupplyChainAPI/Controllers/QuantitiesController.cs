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
    public class QuantitiesController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly IDeliveriesService _deliveriesService;

        public QuantitiesController(IOrdersService ordersService, IDeliveriesService deliveriesService)
        {
            _ordersService = ordersService;
            _deliveriesService = deliveriesService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProcessedOrders>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetQuantities(int? id, string? reference)
        {
            var receivedDeliveries = await _deliveriesService.GetDeliveries();
            var shippedDeliveries = _deliveriesService.FilterByShippedOrders(receivedDeliveries.Value);

            var receivedOrders = await _ordersService.GetOrders();
            var filteredOrders = _ordersService.FilterAndMapByIdOrReference(receivedOrders.Value, id, reference);
            
            var processedOrders = _deliveriesService.GetShippedQuantities(filteredOrders, shippedDeliveries);
            return Ok(processedOrders);
        }
    }
}