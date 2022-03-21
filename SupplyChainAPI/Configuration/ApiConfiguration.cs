namespace SupplyChainAPI.Configuration
{
    public class ApiConfiguration : IApiConfiguration
    {
        public string BaseAddress { get; set; }
        public string BasePath { get; set; }
        public string ApiKeyValue { get; set; }
        public string ApiKeyName { get; set; }
    }
}