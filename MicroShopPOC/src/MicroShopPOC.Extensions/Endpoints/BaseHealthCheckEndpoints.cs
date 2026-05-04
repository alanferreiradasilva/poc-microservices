using MicroShopPOC.Extensions.Endpoints.Abstractions;
using MicroShopPOC.Extensions.Services.Abstractions;
using MicroShopPOC.Extensions.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MicroShopPOC.Extensions.Endpoints
{
    public abstract class BaseHealthCheckEndpoints : IEndpointMapper
    {
        private const string _authApi = "api/auth/health";
        private const string _productsApi = "api/products/health";
        private const string _salesApi = "api/sales/health";

        public virtual Task Map(WebApplication app)
        {
            app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }))
                   .AllowAnonymous();

            app.MapGet("/health/services", async ([FromServices] IHttpClientService httpService) => {

                var startTime = Stopwatch.GetTimestamp();

                var (auth, products, sales) = await (
                    httpService.GetAuthAsync(_authApi),
                    httpService.GetProductsAsync(_productsApi),
                    httpService.GetSalesAsync(_salesApi)
                ).WhenAll();

                var elapsed = Stopwatch.GetElapsedTime(startTime);
                var elapsedTime = elapsed.TotalSeconds;

                var date = DateTime.UtcNow.ToString("yyyyMMdd HH:mm:ss");

                return Results.Ok(new { auth, products, sales, date, elapsedTime = $"{elapsedTime} seconds" });
            })
            .AllowAnonymous();

            return Task.CompletedTask;
        }
    }
}
