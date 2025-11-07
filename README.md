# ENTREGA - 3º SPRINT

## Nomes dos integrantes
- Leonardo Cadena de Souza - rm557528
- Davi Gonzaga - rm554890

## Justificativa do Domínio
Optamos por um domínio de gestão de manutenção automotiva com três entidades principais:
- Moto: item de manutenção e serviço.
- Mecanico: responsável pela execução dos serviços.
- Deposito: local de armazenamento/apoio para peças e logística.

O sistema também inclui `Oficina` para demonstrar outro recurso CRUD. O domínio é simples/familiar e cobre casos típicos de CRUD, além de permitir demonstrar paginação, HATEOAS, versionamento e documentação Swagger.

## Justificativa da Arquitetura
API RESTful em .NET 8 (Web API), com camadas:
- Models: `Moto`, `Mecanico`, `Deposito`, `Oficina`.
- Data: `AppDbContext` com EF Core InMemory.
- Services: regras de negócio/acesso a dados por entidade e `SentimentService` (ML.NET).
- Controllers: endpoints REST com status codes, paginação e HATEOAS.
- Infra: Health Checks, Versionamento de API, Segurança (API Key), Swagger/OpenAPI.

Boas práticas adotadas:
- CRUD completo para as entidades do domínio.
- Paginação e HATEOAS nos endpoints de listagem.
- Swagger/OpenAPI com descrições, exemplos e múltiplas versões (v1).
- Health Checks (`/health`).
- Segurança via API Key.
- Endpoint de ML.NET para demonstrar inferência simples.

## Requisitos e execução
Pré-requisito: .NET 8 SDK instalado.

Executar a API:
```bash
dotnet restore
dotnet run --project Sprint-4/Sprint-4.csproj
```
Swagger (Development):
- http://localhost:5000/swagger
- https://localhost:5001/swagger

### Versionamento
Os endpoints são versionados. Utilize o prefixo `api/v1/...` nas rotas.

### Segurança (API Key)
- A API está protegida por chave de API via header `X-Api-Key`.
- Por padrão, se não for configurada, a chave é `dev-key`.
- Para alterar, defina a configuração `ApiKey` (variável de ambiente ou `appsettings.json`).

Headers nos requests:
```text
X-Api-Key: dev-key
```

Observações:
- As rotas `/health` e `/swagger` não exigem chave.
- No Swagger UI, clique em "Authorize" e use o esquema `ApiKey` informando a mesma chave.

## Exemplos de uso dos endpoints (v1)
- Moto
  - GET  /api/v1/moto?page=1&pageSize=10
  - GET  /api/v1/moto/1
  - POST /api/v1/moto
  - PUT  /api/v1/moto/1
  - DELETE /api/v1/moto/1
- Mecanico
  - GET  /api/v1/mecanico?page=1&pageSize=10
  - GET  /api/v1/mecanico/1
  - POST /api/v1/mecanico
  - PUT  /api/v1/mecanico/1
  - DELETE /api/v1/mecanico/1
- Depósito
  - GET  /api/v1/deposito?page=1&pageSize=10
  - GET  /api/v1/deposito/1
  - POST /api/v1/deposito
  - PUT  /api/v1/deposito/1
  - DELETE /api/v1/deposito/1
- Oficina
  - GET  /api/v1/oficina?page=1&pageSize=10
  - GET  /api/v1/oficina/1
  - POST /api/v1/oficina
  - PUT  /api/v1/oficina/1
  - DELETE /api/v1/oficina/1
- Health Checks
  - GET  /health
- ML.NET (Análise de Sentimento)
  - GET  /api/v1/ml/sentiment?text=ótimo

Exemplo curl (com API Key):
```bash
curl -H "X-Api-Key: dev-key" https://localhost:5001/api/v1/moto
```

## Testes
Foram implementados:
- Testes unitários com xUnit para a lógica principal dos serviços (`MotoService` etc.).
- Testes de integração básicos com `WebApplicationFactory` (incluindo verificação de `/health` e listagem versionada com API Key).

Executar testes:
```bash
# todos os testes
 dotnet test

# ou diretamente no projeto de testes
 dotnet test Sprint-4.Tests/Sprint-4.Tests.csproj
```

Coleta de cobertura (Coverlet):
```bash
 dotnet test --collect:"XPlat Code Coverage"
```

## Observações sobre Swagger
- Documentação atualizada com suporte a múltiplas versões (v1) e esquema de segurança `ApiKey` (header `X-Api-Key`).
- Exemplos de payloads utilizando `Swashbuckle.AspNetCore.Filters`.
