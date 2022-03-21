namespace SupplyChainAPI.Configuration
{
    public interface IApiConfiguration
    {
        string BaseAddress { get; }
        string BasePath { get; }
        string ApiKeyValue { get; }
        string ApiKeyName { get; }
    }
}