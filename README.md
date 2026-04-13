# MicroShopPOC

A proof of concept (POC) for studying distributed microservices architecture with authentication, products, and sales, using Vue 3 on the frontend and .NET Core 10 on the backend.

---

## 🧱 Architecture Overview

- **Frontend:** Vue 3 + TypeScript + Pinia + Vue Router + Tailwind CSS + Composition API
- **API Gateway:** .NET Core 10 Web API with Minimal APIs
- **Microservices:** .NET Core 10 Web API with Minimal APIs

### Microservices

| Project | Responsibility | Database |
|---------|---------------|----------|
| `MicroShopPOC.Auth` | User authentication and JWT issuance | SQLite (Docker) |
| `MicroShopPOC.Products` | Product CRUD and auditing | PostgreSQL (Docker) |
| `MicroShopPOC.Sales` | Sales CRUD and product integration | MongoDB (Docker) |

### Communication
- **Frontend → API Gateway:** REST API
- **API Gateway → Microservices:** REST API
- **Microservices ↔ Microservices:** Internal REST API

### Security
- Only the **API Gateway** is exposed to the internet.
- Microservices run on a private network and communicate internally.
- The **JWT** is validated at the Gateway and propagated to all microservices.
- Each product and sale record stores the `UserId` extracted from the JWT for auditing.

---

## 🗂️ Solution Structure

```
MicroShopPOC/
├── MicroShopPOC.slnx
└── src/
    ├── MicroShopPOC.Api        # API Gateway
    ├── MicroShopPOC.Auth       # Authentication Microservice
    ├── MicroShopPOC.Products   # Products Microservice
    ├── MicroShopPOC.Sales      # Sales Microservice
    ├── MicroShopPOC.Tests      # Test project (TUnit)
    └── frontend/               # Vue 3 frontend
        └── src/
            ├── assets/
            ├── components/
            ├── composables/
            ├── layouts/
            ├── models/
            ├── pages/
            ├── router/
            ├── services/
            ├── stores/
            └── utils/
```

---

## 🧩 Frontend Screens

1. **Login** — user authentication
2. **Home** — overview and navigation
3. **Products List** — full CRUD for products
4. **Sales List** — full CRUD for sales
5. **Users List** — view registered users

---

## 🔄 Authentication Flow

1. Frontend sends credentials to the API Gateway.
2. Gateway forwards to the Auth microservice.
3. Auth Service validates credentials and issues a JWT containing `UserId`.
4. Gateway returns the token to the frontend.
5. Frontend stores the JWT and includes it in all subsequent requests.
6. Microservices extract the `UserId` from the token and record it in created data.

---

## ⚙️ Requirements

### Backend
- **Visual Studio 2022** (or later) with the **ASP.NET and web development** workload
- **.NET 10 SDK** — [Download](https://dotnet.microsoft.com/download/dotnet/10.0)
- **Docker Desktop** — to run SQLite, PostgreSQL, and MongoDB containers

### Frontend
- **Node.js** 20+ and **npm**

---

## 🚀 Getting Started

### 1. Configure Secrets (Development)

All sensitive keys and connection strings are managed via `dotnet user-secrets`. Run the following in the `MicroShopPOC.Api` project directory:

```bash
dotnet user-secrets set "MicroShopPOC:JwtKey" "MicroShopPOC-secret-key"
dotnet user-secrets set "MicroShopPOC:AuthDb" "Data Source=auth.db"
dotnet user-secrets set "MicroShopPOC:ProductsDb" "<postgres-connection-string>"
dotnet user-secrets set "MicroShopPOC:SalesDb" "<mongodb-connection-string>"
```

> **Note:** Never commit secrets to version control. Use `user-secrets` for development and environment variables for production.

### 2. Start the Databases (Docker)

```bash
docker compose up -d
```

### 3. Run the Backend

Open `MicroShopPOC.slnx` in Visual Studio 2022 and run the solution, or use the CLI:

```bash
dotnet run --project src/MicroShopPOC.Api
```

### 4. Run the Frontend

```bash
cd src/frontend
npm install
npm run dev
```

---

## 🔐 Secrets Management

| Environment | Strategy |
|-------------|----------|
| Development | `dotnet user-secrets` |
| Production  | Environment variables |

`appsettings.json` is used only for non-sensitive configuration values.

---

## 🧪 Testing

Tests are implemented using **TUnit** and cover:
- Unit tests for Repositories, Services, and API Endpoints
- Integration tests for the full flow via the API Gateway

```bash
dotnet test src/MicroShopPOC.Tests
```

---

## ✅ Expected Outcome

- Distributed authentication via JWT
- Secure communication between API Gateway and microservices
- Data isolation per microservice
- Unit and integration tests ensuring quality
- Fully integrated and functional frontend
