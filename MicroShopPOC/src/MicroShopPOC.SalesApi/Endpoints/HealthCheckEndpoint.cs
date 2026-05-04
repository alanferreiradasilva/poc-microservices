using MicroShopPOC.Extensions.Domain.Entities;
using MicroShopPOC.Extensions.Endpoints.Abstractions;

namespace MicroShopPOC.SalesApi.Endpoints
{
    public class HealthCheckEndpoint : IEndpointMapper
    {
        public Task Map(WebApplication app)
        {
            app.MapGet("/api/sales/health", () => {
                return Results.Ok(HealthCheckDto.GetHealthy());
            })
            .AllowAnonymous();

            return Task.CompletedTask;
        }
    }
}
