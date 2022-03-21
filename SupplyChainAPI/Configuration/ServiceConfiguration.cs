namespace SupplyChainAPI.Configuration
{
    public class ServiceConfiguration : IServiceConfiguration
    {
        public ServiceConfiguration()
        {
            OrdersApi = new ApiConfiguration();
            DeliveriesApi = new ApiConfiguration();
        }

        public IApiConfiguration OrdersApi { get; set; }
        public IApiConfiguration DeliveriesApi { get; set; }
    }
}