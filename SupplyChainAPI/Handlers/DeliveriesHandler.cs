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

    public class DeliveriesHandler : IDeliveriesHandler
    {
        //TO-DO: use config
        private const string Endpoint = "https://case-study-challenges.s3-eu-west-1.amazonaws.com/BE/deliveries.json";
        private readonly HttpClient _httpClient;

        public DeliveriesHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<DeliveriesResponse>> RetrieveDeliveries()
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
                    ? Result.Success(JsonConvert.DeserializeObject<DeliveriesResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerSettings
                    {
                        ContractResolver = contractResolver,
                        Formatting = Formatting.Indented
                    }))
                    : Result.Failure<DeliveriesResponse>($"{nameof(RetrieveDeliveries)} responded with {nameof(response.StatusCode)} {response.StatusCode}.");
            }
            catch (Exception ex)
            {
                return Result.Failure<DeliveriesResponse>(ex.Message);
            }
        }
    }
}
