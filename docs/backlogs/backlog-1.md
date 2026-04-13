# Backlog de Desenvolvimento – POC MicroShopPOC

## 🎯 Objetivo
Implementar a POC de arquitetura distribuída com frontend Vue 3, API Gateway em .NET Core e três microserviços (Auth, Products, Sales), seguindo boas práticas de desenvolvimento, segurança e testes.

---

## 🧱 Estrutura da Solution
MicroShopPOC/
├── MicroShopPOC.sln
├── src/
│   ├── MicroShopPOC.Api        (API Gateway)
│   ├── MicroShopPOC.Auth       (Microserviço de Autenticação)
│   ├── MicroShopPOC.Products   (Microserviço de Produtos)
│   ├── MicroShopPOC.Sales      (Microserviço de Vendas)
│   └── MicroShopPOC.Tests      (Projeto de testes com TUnit)
|──frontend/                    (vue project)
│   ├── src/


---

## 📌 Tarefas por Projeto

### 1. **API Gateway (MicroShopPOC.Api)**
- [ ] Configurar minimal APIs em .NET Core 10.
- [ ] Implementar roteamento para Auth, Products e Sales.
- [ ] Configurar autenticação JWT (validação de tokens).
- [ ] Adicionar middleware de logging e tratamento de erros.
- [ ] Configurar secrets e connection strings via **user-secrets** (dev) e **variáveis de ambiente** (prod).

---

### 2. **Auth Service (MicroShopPOC.Auth)**
- [ ] Criar modelo `AppUser` com propriedades básicas (Id, Username, PasswordHash).
- [ ] Configurar EF Core com migrations.
- [ ] Implementar repository pattern genérico.
- [ ] Implementar serviços de autenticação (login, emissão de JWT).
- [ ] Usar **Mapster** para mapear entidades ↔ DTOs.
- [ ] Criar seed inicial com usuário default:  
  - Username: `admin`  
  - Senha: `admin@123`
- [ ] Configurar banco de dados SQLite via Docker.

---

### 3. **Product Service (MicroShopPOC.Products)**
- [ ] Criar modelo `Product` (Id, Name, Price, CreatedByUserId).
- [ ] Configurar EF Core com migrations.
- [ ] Implementar repository pattern genérico.
- [ ] Implementar serviços de CRUD de produtos.
- [ ] Usar **Mapster** para mapear entidades ↔ DTOs.
- [ ] Configurar banco de dados PostgreSQL via Docker.

---

### 4. **Sales Service (MicroShopPOC.Sales)**
- [ ] Criar modelo `Sale` (Id, ProductId, Amount, UserId).
- [ ] Configurar EF Core com migrations.
- [ ] Implementar repository pattern genérico.
- [ ] Implementar serviços de CRUD de vendas.
- [ ] Usar **Mapster** para mapear entidades ↔ DTOs.
- [ ] Configurar banco de dados MongoDB via Docker.

---

### 5. **Projeto de Testes (MicroShopPOC.Tests)**
- [ ] Configurar projeto como Class Library .NET Core.
- [ ] Instalar **TUnit** via NuGet.
- [ ] Planejar testes antes da implementação (TDD).
- [ ] Criar estrutura de pastas:
MicroShopPOC.Tests/
├── Auth/
├── Products/
├── Sales/
└── Common/

- [ ] Implementar testes unitários para:
- Repositórios (CRUD básico).
- Services (regras de negócio).
- APIs (endpoints).
- [ ] Implementar testes de integração (fluxo completo via Gateway).
- [ ] Validar autenticação e propagação de `UserId`.

---

## 🔄 Fluxo de Comunicação
- **Frontend → API Gateway:** REST API.
- **API Gateway → Microserviços:** REST API.
- **Microserviços ↔ Microserviços:** REST API interna.
- Fluxo interno: **API → Service → Repository → Database**.

---

## 🚀 Próximos Passos
1. Configurar bancos via Docker (SQLite, PostgreSQL, MongoDB).
2. Implementar Auth Service com seed de usuário admin.
3. Implementar Products e Sales com CRUD e auditoria (`UserId`).
4. Configurar API Gateway com autenticação JWT.
5. Implementar frontend Vue com telas de login, home, produtos, vendas e usuários.
6. Escrever testes unitários e de integração com TUnit.
7. Validar fluxo completo de login → produtos → vendas.

---

## ✅ Resultado Esperado
- POC funcional com autenticação distribuída via JWT.
- Comunicação segura entre Gateway e microserviços.
- Isolamento de dados por serviço.
- Testes unitários e de integração garantindo qualidade.
- Frontend integrado e funcional.
