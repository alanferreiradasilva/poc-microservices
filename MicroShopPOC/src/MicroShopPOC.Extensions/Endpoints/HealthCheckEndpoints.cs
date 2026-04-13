using MicroShopPOC.Extensions.Endpoints.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MicroShopPOC.Extensions.Endpoints
{
    public abstract class BaseHealthCheckEndpoints : IEndpointMapper
    {
        public virtual Task Map(WebApplication app)
        {
            app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }))
                   .AllowAnonymous();

            return Task.CompletedTask;
        }
    }
}
