using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using MicroShopPOC.AuthApi;

namespace MicroShopPOC.Tests.Common;

public class JwtValidationTests
{
    private static WebApplicationFactory<Program> CreateFactory() =>
        new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test");
            builder.ConfigureAppConfiguration((_, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["MicroShopPOC:JwtKey"] = "test-secret-key-at-least-32-characters-long",
                    ["MicroShopPOC:AuthTestDbName"] = Guid.NewGuid().ToString()
                });
            });
        });

    [Test]
    public async Task JwtToken_IsValidAndContainsUsername()
    {
        await using var factory = CreateFactory();
        // Admin user is seeded by Program.cs startup
        var client = factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/auth/login", new { username = "admin", password = "admin@123" });

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);

        var body = await response.Content.ReadFromJsonAsync<TokenResponse>();
        await Assert.That(body?.Token).IsNotNull();

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(body!.Token);

        var usernameClaim = jwt.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
        await Assert.That(usernameClaim).IsEqualTo("admin");
        await Assert.That(jwt.ValidTo).IsGreaterThan(DateTime.UtcNow);
    }
}
