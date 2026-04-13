# Development Backlog – POC MicroShopPOC

## 🎯 Objective
Implement the distributed architecture POC with a Vue 3 frontend, .NET Core API Gateway, and three microservices (Auth, Products, Sales), following development best practices, security, and testing.

---

## 🧱 Solution Structure
MicroShopPOC/
├── MicroShopPOC.sln
├── src/
│   ├── MicroShopPOC.Api        (API Gateway)
│   ├── MicroShopPOC.Auth       (Authentication Microservice)
│   ├── MicroShopPOC.Products   (Products Microservice)
│   ├── MicroShopPOC.Sales      (Sales Microservice)
│   └── MicroShopPOC.Tests      (Test project with TUnit)
|──frontend/                    (vue project)
│   ├── src/


---

## 📌 Tasks per Project

### 1. **API Gateway (MicroShopPOC.Api)**
- [ ] Configure Minimal APIs in .NET Core 10.
- [ ] Implement routing for Auth, Products, and Sales.
- [ ] Configure JWT authentication (token validation).
- [ ] Add logging middleware and error handling.
- [ ] Configure secrets and connection strings via **user-secrets** (dev) and **environment variables** (prod).

---

### 2. **Auth Service (MicroShopPOC.Auth)**
- [ ] Create `AppUser` model with basic properties (Id, Username, PasswordHash).
- [ ] Configure EF Core with migrations.
- [ ] Implement generic repository pattern.
- [ ] Implement authentication services (login, JWT issuance).
- [ ] Use **Mapster** to map entities ↔ DTOs.
- [ ] Create initial seed with default admin user:  
  - Username: `admin`  
  - Password: `admin@123`
- [ ] Configure SQLite database via Docker.

---

### 3. **Product Service (MicroShopPOC.Products)**
- [ ] Create `Product` model (Id, Name, Price, CreatedByUserId).
- [ ] Configure EF Core with migrations.
- [ ] Implement generic repository pattern.
- [ ] Implement product CRUD services.
- [ ] Use **Mapster** to map entities ↔ DTOs.
- [ ] Configure PostgreSQL database via Docker.

---

### 4. **Sales Service (MicroShopPOC.Sales)**
- [ ] Create `Sale` model (Id, ProductId, Amount, UserId).
- [ ] Configure EF Core with migrations.
- [ ] Implement generic repository pattern.
- [ ] Implement sales CRUD services.
- [ ] Use **Mapster** to map entities ↔ DTOs.
- [ ] Configure MongoDB database via Docker.

---

### 5. **Test Project (MicroShopPOC.Tests)**
- [ ] Configure project as a .NET Core Class Library.
- [ ] Install **TUnit** via NuGet.
- [ ] Plan tests before implementation (TDD).
- [ ] Create folder structure:
MicroShopPOC.Tests/
├── Auth/
├── Products/
├── Sales/
└── Common/

- [ ] Implement unit tests for:
- Repositories (basic CRUD).
- Services (business rules).
- APIs (endpoints).
- [ ] Implement integration tests (full flow via Gateway).
- [ ] Validate authentication and `UserId` propagation.

---

## 🔄 Communication Flow
- **Frontend → API Gateway:** REST API.
- **API Gateway → Microservices:** REST API.
- **Microservices ↔ Microservices:** Internal REST API.
- Internal flow: **API → Service → Repository → Database**.

---

## 🚀 Next Steps
1. Configure databases via Docker (SQLite, PostgreSQL, MongoDB).
2. Implement Auth Service with admin user seed.
3. Implement Products and Sales with CRUD and auditing (`UserId`).
4. Configure API Gateway with JWT authentication.
5. Implement Vue frontend with login, home, products, sales, and users screens.
6. Write unit and integration tests with TUnit.
7. Validate the complete flow of login → products → sales.

---

## ✅ Expected Outcome
- Functional POC with distributed authentication via JWT.
- Secure communication between Gateway and microservices.
- Data isolation per service.
- Unit and integration tests ensuring quality.
- Integrated and functional frontend.
