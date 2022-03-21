namespace SupplyChainAPI.Handlers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Domain;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class OrdersHandler : IOrdersHandler
    {
        //TO-DO: use config
        private const string Endpoint = "https://case-study-challenges.s3-eu-west-1.amazonaws.com/BE/orders.json";
        private readonly HttpClient _httpClient;

        public OrdersHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<OrderResponse>> RetrieveOrders()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{Endpoint}");
                var content = await response.Content.ReadAsStringAsync();

                var contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };

                var data = JsonConvert.DeserializeObject<OrderResponse>(content);
                return response.StatusCode == HttpStatusCode.OK
                    ? Result.Success(JsonConvert.DeserializeObject<OrderResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerSettings
                    {
                        ContractResolver = contractResolver,
                        Formatting = Formatting.Indented
                    }))
                    : Result.Failure<OrderResponse>($"{nameof(RetrieveOrders)} responded with {nameof(response.StatusCode)} {response.StatusCode}.");
            }
            catch (Exception ex)
            {
                return Result.Failure<OrderResponse>(ex.Message);
            }
        }
    }
}