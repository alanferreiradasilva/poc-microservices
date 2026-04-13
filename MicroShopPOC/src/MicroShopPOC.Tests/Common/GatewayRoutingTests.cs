using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using MicroShopPOC.AuthApi;

namespace MicroShopPOC.Tests.Common;

public class GatewayRoutingTests
{
    [Test]
    public async Task AuthApi_LoginEndpoint_IsReachable()
    {
        await using var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
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

        // Admin user is seeded by Program.cs startup
        var client = factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/auth/login",
            new { username = "admin", password = "admin@123" });

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }
}
