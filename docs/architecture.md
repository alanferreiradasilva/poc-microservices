# Plano de Desenvolvimento de POC – Frontend + API Gateway + 3 Microserviços

## 🎯 Objetivo
Desenvolver uma prova de conceito (POC) para estudar arquitetura de microserviços com autenticação, produtos e vendas, utilizando Vue 3 no frontend e .NET Core 10 no backend.

---

## 🧱 Arquitetura Geral
- **Frontend:** Vue 3 + TypeScript + Pinia + Composition API.
- **API Gateway:** .NET Core 10 Web API com Minimal APIs.
- **Microserviços:** .NET Core 10 Web API com Minimal APIs.

### Microserviços
1. **Autenticação:** responsável por login e emissão de JWT.
2. **Produtos:** CRUD de produtos.
3. **Vendas:** CRUD de vendas e integração com produtos.

### Comunicação
- **Frontend → API Gateway:** via REST API.
- **API Gateway → Microserviços:** via REST API.
- **Microserviços ↔ Microserviços:** via REST API interna.

### Segurança e Rede
- Apenas o **API Gateway** é exposto à internet.
- Os **microserviços** ficam em rede privada e se comunicam internamente.
- O **JWT** é validado no Gateway e propagado para os microserviços.

---

## 🗄️ Bases de Dados
Cada microserviço possui sua base dedicada, garantindo isolamento e autonomia.

| Microserviço | Função | Provedor sugerido | Observações |
|---------------|--------|-------------------|--------------|
| **Autenticação** | Armazenar usuários e credenciais | **SQLite** | Simples, leve e ideal para POC inicial. |
| **Produtos** | Armazenar produtos e auditoria | **PostgreSQL** | Robusto, relacional e com suporte avançado a JSON. |
| **Vendas** | Armazenar registros de vendas | **MongoDB** | Flexível e escalável para dados semi-estruturados. |

Cada registro de produto ou venda deve conter o `UserId` extraído do JWT para auditoria.

Para mantermos todas estas bases vamos necessitar subir elas em conteiners Docker para facilitar o desenvolvimento.

---

## 🔐 Gestão de Secrets e Connection Strings

- **Ambiente de Desenvolvimento:**  
Todas as keys e connection strings devem ser configuradas usando o **dotnet user-secrets**.  
Isso evita expor credenciais sensíveis em arquivos de configuração ou no controle de versão.  

Exemplo de configuração:  
```bash
dotnet user-secrets set "MicroShopPOC:JwtKey" "MicroShopPOC-secret-key"
dotnet user-secrets set "MicroShopPOC:AuthDb" "Data Source=auth.db"
dotnet user-secrets set "MicroShopPOC:ProductsDb" "..."
dotnet user-secrets set "MicroShopPOC:SalesDb" "..."
```

Boas práticas:
Nunca versionar secrets em arquivos appsettings.json.
Usar appsettings.json apenas para valores não sensíveis.

Configurar o Program.cs para ler secrets de acordo com o ambiente:
```csharp
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables();
```

👉 Assim você garante que **em desenvolvimento** os secrets ficam no `user-secrets` e **em produção** nas variáveis de ambiente, sem risco de expor credenciais.

---

## 🔄 Fluxo de Autenticação e Autorização
1. **Login:** Frontend envia credenciais para o API Gateway.
2. **Gateway:** encaminha para o microserviço de autenticação.
3. **Auth Service:** valida credenciais e emite JWT com `UserId`.
4. **Gateway:** retorna o token ao frontend.
5. **Frontend:** armazena o JWT e envia em todas as requisições subsequentes.
6. **Microserviços:** extraem o `UserId` do token e o registram nos dados criados.

---

## Como os Microserviços são estruturados.
- Classes, interfaces, Dtos, Serviços, Repositórios e Endpoints das minimal apis devem seguir um padrão de nomenclatura.

Exemplo: Para o microserviço de Produto, todo recurso que vier dele tera as nomenclaturas:
- Product.cs e ProductDto.cs.
- IProductService e ProductService.cs.
- IProductRepository e ProductRepository.cs.
- ProductEndpoints.cs (minial api).

O DbContext como é único por projeto de microserviço deve conter o nome do microserviço `AppProductDbContext.cs`.

---

## Minimal Apis
- All endpoinsts should be implements: `MicroShopPOC.Extensions.Endpoints.Abstractions.IEndpointMapper`

```chsarp
using MicroShopPOC.Extensions.Endpoints.Abstractions;

public class MyCustomEnpoints : IEndpointMapper {
    
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

All Program.cs from api gateway and microservices should have before `app.Run()`:

```csahrp
using MicroShopPOC.Extensions.Endpoints;

app.RegisterEndpoints();
```

---

## 🧩 Telas do Frontend
1. **Login:** autenticação do usuário.
2. **Home:** visão geral e navegação.
3. **Lista de Produtos:** CRUD completo de produtos.
4. **Lista de Vendas:** CRUD completo de vendas.
5. **Lista de Usuários:** visualização dos usuários cadastrados.

---

## ⚙️ Comunicação Interna
- **API Gateway:** roteia requisições para os microserviços conforme configuração.
- **Microserviços:** podem se comunicar diretamente via HTTP interno.
- **Mensageria (opcional):** pode ser adicionada futuramente para eventos assíncronos (RabbitMQ ou Kafka).

---

## 🧠 Auditoria e Contexto de Usuário
- O `UserId` é propagado via JWT e armazenado localmente em cada serviço.
- Não há relacionamento direto entre bancos.
- Para consultas agregadas, o Gateway pode compor respostas chamando múltiplos serviços.

---

## 🚀 Próximos Passos
1. Criar estrutura base dos projetos (.NET e Vue).
2. Implementar Auth Service com JWT.
3. Configurar API Gateway com Ocelot ou YARP.
4. Criar microserviços de Produtos e Vendas.
5. Implementar frontend com autenticação e CRUDs.
6. Testar fluxo completo de login, produtos e vendas.

---

## ✅ Resultado Esperado
Uma POC funcional demonstrando:
- Autenticação distribuída via JWT.
- Comunicação segura entre Gateway e microserviços.
- Isolamento de dados por serviço.
- Frontend integrado e funcional.
