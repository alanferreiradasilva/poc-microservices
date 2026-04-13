using MicroShopPOC.Extensions.Endpoints.Abstractions;

namespace MicroShopPOC.Api;

public class AuthProxyEndpoints : IEndpointMapper
{
    public Task Map(WebApplication app)
    {
        app.MapPost("/api/auth/login", async (HttpRequest request, IHttpClientFactory clientFactory) =>
        {
            var client = clientFactory.CreateClient("AuthApi");
            var body = await new StreamReader(request.Body).ReadToEndAsync();
            var content = new StringContent(body, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/auth/login", content);
            var result = await response.Content.ReadAsStringAsync();
            return Results.Content(result, "application/json", statusCode: (int)response.StatusCode);
        }).AllowAnonymous();

        app.MapPost("/api/auth/register", async (HttpRequest request, IHttpClientFactory clientFactory) =>
        {
            var client = clientFactory.CreateClient("AuthApi");
            var body = await new StreamReader(request.Body).ReadToEndAsync();
            var content = new StringContent(body, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/auth/register", content);
            var result = await response.Content.ReadAsStringAsync();
            return Results.Content(result, "application/json", statusCode: (int)response.StatusCode);
        }).AllowAnonymous();

        return Task.CompletedTask;
    }
}
