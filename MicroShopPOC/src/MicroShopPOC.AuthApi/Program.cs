using Microsoft.EntityFrameworkCore;
using MicroShopPOC.AuthApi;
using MicroShopPOC.Extensions.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables();

if (builder.Environment.IsEnvironment("Test"))
{
    var testDbName = builder.Configuration["MicroShopPOC:AuthTestDbName"] ?? "AuthTestDb";
    builder.Services.AddDbContext<AppAuthDbContext>(options =>
        options.UseInMemoryDatabase(testDbName));
}
else
{
    builder.Services.AddDbContext<AppAuthDbContext>(options =>
        options.UseSqlite(builder.Configuration["MicroShopPOC:AuthDb"] ?? "Data Source=auth.db"));
}

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Ensure database is created and seed admin user
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppAuthDbContext>();
    db.Database.EnsureCreated();

    if (!db.Users.Any())
    {
        db.Users.Add(new AppUser
        {
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin@123")
        });
        db.SaveChanges();
    }
}

app.RegisterEndpoints(typeof(Program).Assembly);

app.Run();

public partial class Program { }
