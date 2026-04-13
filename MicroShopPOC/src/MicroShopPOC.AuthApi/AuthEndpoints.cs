using MicroShopPOC.Extensions.Endpoints.Abstractions;

namespace MicroShopPOC.AuthApi;

public class AuthEndpoints : IEndpointMapper
{
    public Task Map(WebApplication app)
    {
        app.MapPost("/api/auth/login", async (LoginRequest request, IAuthService authService) =>
        {
            var result = await authService.LoginAsync(request);
            return result is null
                ? Results.Unauthorized()
                : Results.Ok(result);
        }).AllowAnonymous();

        app.MapPost("/api/auth/register", async (RegisterRequest request, IAuthService authService) =>
        {
            var result = await authService.RegisterAsync(request);
            return result is null
                ? Results.Conflict(new { error = "Username already exists." })
                : Results.Ok(result);
        }).AllowAnonymous();

        return Task.CompletedTask;
    }
}
