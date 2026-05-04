using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MicroShopPOC.Extensions.Endpoints;
using Scalar.AspNetCore;
using MicroShopPOC.Extensions.Services.Abstractions;
using MicroShopPOC.Extensions.Services.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables();

var jwtKey = builder.Configuration["MicroShopPOC:JwtKey"]!;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "MicroShopPOC",
            ValidAudience = "MicroShopPOC",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((doc, _, _) =>
    {
        doc.Info.Title = "MicroShopPOC API Gateway";
        doc.Info.Version = "v1";
        return Task.CompletedTask;
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var dic = new Dictionary<string, string>
{
    ["AuthApi"] = "http://localhost:5001",
    ["ProductsApi"] = "http://localhost:5002",
    ["SalesApi"] = "http://localhost:5003"
};

foreach (var item in dic)
{
    builder.Services.AddHttpClient(item.Key, client =>
    {
        client.BaseAddress = new Uri(item.Value);
    });
}

builder.Services.AddScoped<IHttpClientService, HttpClientService>();

var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapOpenApi();
app.MapScalarApiReference();

app.RegisterEndpoints(typeof(Program).Assembly);

app.Run();
