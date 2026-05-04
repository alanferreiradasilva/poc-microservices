using MicroShopPOC.Extensions.Domain.Entities;
using MicroShopPOC.Extensions.Services.Abstractions;
using System.Net.Http.Json;
using System.Text.Json;

namespace MicroShopPOC.Extensions.Services.Core
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _factory;

        public HttpClientService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<HealthCheckDto?> GetAuthAsync(string url) =>
            await GetAsync("/api/auth/health", "AuthApi");
        
        public async Task<HealthCheckDto?> GetProductsAsync(string url) =>
            await GetAsync("/api/products/health", "ProductsApi");
        public async Task<HealthCheckDto?> GetSalesAsync(string url) =>
            await GetAsync("/api/sales/health", "SalesApi");

        private async Task<HealthCheckDto?> GetAsync(string url, string clientName)
        {
            try
            {
                var client = _factory.CreateClient(clientName);
                var content = new StringContent(string.Empty, System.Text.Encoding.UTF8, "application/json");
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<HealthCheckDto>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return result;
                }
                return HealthCheckDto.GetUnhealthy();
            }
            catch (HttpRequestException)
            {
                return HealthCheckDto.GetUnhealthy();
            }
        }
    }
}
