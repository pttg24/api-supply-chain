namespace SupplyChainAPI.Configuration
{

    public interface IServiceConfiguration
    {
        public IApiConfiguration OrdersApi { get; set; }
        public IApiConfiguration DeliveriesApi { get; set; }
    }
}