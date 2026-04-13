using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using MicroShopPOC.AuthApi;

namespace MicroShopPOC.Tests.Microservices.Auth;

public class RegisterTests
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
    public async Task Register_NewUser_ReturnsToken()
    {
        await using var factory = CreateFactory();
        var client = factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/auth/register", new { username = "newuser", password = "password123" });

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
        var body = await response.Content.ReadFromJsonAsync<TokenResponse>();
        await Assert.That(body?.Token).IsNotNull();
        await Assert.That(body!.Token).IsNotEmpty();
    }

    [Test]
    public async Task Register_DuplicateUsername_Returns409()
    {
        await using var factory = CreateFactory();
        // Admin user is already seeded by Program.cs startup, try to register "admin" again
        var client = factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/auth/register", new { username = "admin", password = "anotherpass" });

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.Conflict);
    }
}

