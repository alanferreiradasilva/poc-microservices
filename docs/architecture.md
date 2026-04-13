# Development Plan for POC – Frontend + API Gateway + 3 Microservices

## 🎯 Objective
Develop a proof of concept (POC) to study microservices architecture with authentication, products, and sales, using Vue 3 on the frontend and .NET Core 10 on the backend.

---

## 🧱 General Architecture
- **Frontend:** Vue 3 + TypeScript + Pinia + Composition API.
- **API Gateway:** .NET Core 10 Web API with Minimal APIs.
- **Microservices:** .NET Core 10 Web API with Minimal APIs.

### Microservices
1. **Authentication:** responsible for login and JWT issuance.
2. **Products:** CRUD for products.
3. **Sales:** CRUD for sales and integration with products.

### Communication
- **Frontend → API Gateway:** via REST API.
- **API Gateway → Microservices:** via REST API.
- **Microservices ↔ Microservices:** via internal REST API.

### Security and Network
- Only the **API Gateway** is exposed to the internet.
- **Microservices** are on a private network and communicate internally.
- The **JWT** is validated at the Gateway and propagated to the microservices.

---

## 🗄️ Databases
Each microservice has its own dedicated database, ensuring isolation and autonomy.

| Microservice | Function | Suggested Provider | Notes |
|---------------|--------|-------------------|--------------|
| **Authentication** | Store users and credentials | **SQLite** | Simple, lightweight, and ideal for the initial POC. |
| **Products** | Store products and auditing data | **PostgreSQL** | Robust, relational, with advanced JSON support. |
| **Sales** | Store sales records | **MongoDB** | Flexible and scalable for semi-structured data. |

Each product or sale record must contain the `UserId` extracted from the JWT for auditing.

To keep all these databases running, we need to spin them up in Docker containers to ease development.

---

## 🔐 Secrets and Connection Strings Management

- **Development Environment:**  
All keys and connection strings must be configured using **dotnet user-secrets**.  
This avoids exposing sensitive credentials in configuration files or version control.  

Example configuration:  
```bash
dotnet user-secrets set "MicroShopPOC:JwtKey" "MicroShopPOC-secret-key"
dotnet user-secrets set "MicroShopPOC:AuthDb" "Data Source=auth.db"
dotnet user-secrets set "MicroShopPOC:ProductsDb" "..."
dotnet user-secrets set "MicroShopPOC:SalesDb" "..."
```

Best practices:
Never version secrets in appsettings.json files.
Use appsettings.json only for non-sensitive values.

Configure Program.cs to read secrets according to the environment:
```csharp
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables();
```

👉 This ensures that **in development** secrets are stored in `user-secrets` and **in production** in environment variables, with no risk of exposing credentials.

---

## 🔄 Authentication and Authorization Flow
1. **Login:** Frontend sends credentials to the API Gateway.
2. **Gateway:** forwards to the authentication microservice.
3. **Auth Service:** validates credentials and issues JWT with `UserId`.
4. **Gateway:** returns the token to the frontend.
5. **Frontend:** stores the JWT and sends it in all subsequent requests.
6. **Microservices:** extract the `UserId` from the token and record it in created data.

---

## How Microservices are Structured
- Classes, interfaces, DTOs, Services, Repositories, and Endpoints of the Minimal APIs must follow a naming convention.

Example: For the Product microservice, all resources from it will have the following names:
- Product.cs and ProductDto.cs.
- IProductService and ProductService.cs.
- IProductRepository and ProductRepository.cs.
- ProductEndpoints.cs (minimal API).

The DbContext, since it is unique per microservice project, should contain the microservice name: `AppProductDbContext.cs`.

---

## Minimal APIs
- All endpoints should implement: `MicroShopPOC.Extensions.Endpoints.Abstractions.IEndpointMapper`

```csharp
using MicroShopPOC.Extensions.Endpoints.Abstractions;

public class MyCustomEndpoints : IEndpointMapper {
    
    public async Task Map(WebApplication app)
    {
        await MyMethod1(app);
        await MyMethod2(app);        
    }

    private async Task MyMethod1(WebApplication app)
    {
        app.MapGet("/api/my-custom/action-name"
        //...
    }

    private async Task MyMethod2(WebApplication app)
    {
        app.MapPost("/api/my-custom/action-name"
        //...
    }
}
```

All Program.cs files from the API gateway and microservices should have before `app.Run()`:

```csharp
using MicroShopPOC.Extensions.Endpoints;

app.RegisterEndpoints();
```

---

## 🧩 Frontend Screens
1. **Login:** user authentication.
2. **Home:** overview and navigation.
3. **Products List:** full CRUD for products.
4. **Sales List:** full CRUD for sales.
5. **Users List:** view registered users.

---

## ⚙️ Internal Communication
- **API Gateway:** routes requests to microservices based on configuration.
- **Microservices:** can communicate directly via internal HTTP.
- **Messaging (optional):** can be added in the future for asynchronous events (RabbitMQ or Kafka).

---

## 🧠 Auditing and User Context
- The `UserId` is propagated via JWT and stored locally in each service.
- There is no direct relationship between databases.
- For aggregated queries, the Gateway can compose responses by calling multiple services.

---

## 🚀 Next Steps
1. Create the base structure for the projects (.NET and Vue).
2. Implement Auth Service with JWT.
3. Configure API Gateway with Ocelot or YARP.
4. Create Products and Sales microservices.
5. Implement frontend with authentication and CRUDs.
6. Test the complete flow of login, products, and sales.

---

## ✅ Expected Outcome
A functional POC demonstrating:
- Distributed authentication via JWT.
- Secure communication between Gateway and microservices.
- Data isolation per service.
- Integrated and functional frontend.
