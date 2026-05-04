using MicroShopPOC.Extensions.Domain.Entities;
using System.Net.Http.Json;

namespace MicroShopPOC.Extensions.Services.Abstractions
{
    public interface IHttpClientService
    {
        Task<HealthCheckDto?> GetAuthAsync(string url);
        Task<HealthCheckDto?> GetProductsAsync(string url);
        Task<HealthCheckDto?> GetSalesAsync(string url);
    }

    public class ExternalApiService
    {
        private readonly HttpClient _authClient;
        private readonly HttpClient _produtcsClient;
        private readonly HttpClient _salesClient;

        public ExternalApiService(IHttpClientFactory factory)
        {
            _authClient = CreateClient(factory, nameof(HttpClientFactoryType.Auth), "https://localhost:5001/");
            _produtcsClient = CreateClient(factory, nameof(HttpClientFactoryType.Auth), "https://localhost:5002/");
            _salesClient  = CreateClient(factory, nameof(HttpClientFactoryType.Sales), "https://localhost:5003/");
        }

        private static HttpClient CreateClient(IHttpClientFactory factory, string nome, string baseUrl)
        {
            var client = factory.CreateClient(nome);
            client.BaseAddress = new Uri(baseUrl);
            return client;
        }

        public async Task<HealthCheckDto?> GetAuthAsync() =>
            await _authClient.GetFromJsonAsync<HealthCheckDto>("/api/auth/health");

        public async Task<HealthCheckDto?> GetProductsAsync() =>
            await _produtcsClient.GetFromJsonAsync<HealthCheckDto>("/api/products/health");

        public async Task<HealthCheckDto?> GetSalesAsync() =>
            await _salesClient.GetFromJsonAsync<HealthCheckDto>("/api/sales/health");    
    }
}
