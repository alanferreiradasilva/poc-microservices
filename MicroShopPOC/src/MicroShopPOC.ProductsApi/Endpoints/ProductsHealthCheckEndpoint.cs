using MicroShopPOC.Extensions.Domain.Entities;
using MicroShopPOC.Extensions.Endpoints.Abstractions;

namespace MicroShopPOC.ProductsApi.Endpoints
{
    public class ProductsHealthCheckEndpoint : IEndpointMapper
    {
        public Task Map(WebApplication app)
        {
            app.MapGet("/api/products/health", () => {
                return Results.Ok(HealthCheckDto.GetHealthy());
            })
            .AllowAnonymous();

            return Task.CompletedTask;
        }
    }
}
