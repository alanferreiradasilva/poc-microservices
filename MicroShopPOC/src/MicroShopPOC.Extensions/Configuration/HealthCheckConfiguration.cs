using MicroShopPOC.Extensions.Domain.Entities;
using Microsoft.AspNetCore.Builder;

namespace MicroShopPOC.Extensions.Configuration
{
    public static class HealthCheckConfiguration
    {
        private static HealthCheckApiDto? _configuration;
        public static HealthCheckApiDto? Configuration { get => _configuration; private set; }

        public static void Initialize(WebApplicationBuilder builder)
        {
            string? jsonConfig = builder.Configuration["MicroShopPOC:HealthApy"];
            _configuration = System.Text.Json.JsonSerializer.Deserialize<HealthCheckApiDto?>(jsonConfig);
        }
    }
}
