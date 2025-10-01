# ENTREGA - 3º SPRINT

## Nomes dos integrantes
- [Integrante 1]
- [Integrante 2]
- [Integrante 3]

## Justificativa do Domínio
Optamos por um domínio de gestão de manutenção automotiva com três entidades:
- Moto: item de manutenção e serviço.
- Mecanico: responsável pela execução dos serviços.
- Deposito: local de armazenamento/apoio para peças e logística.
Esse domínio é simples, familiar e cobre casos típicos de CRUD, além de permitir demonstrar paginação, HATEOAS e documentação Swagger com clareza.

## Justificativa da Arquitetura
API RESTful em .NET 8 (Web API), com camadas:
- Models: `Moto`, `Mecanico`, `Deposito`.
- Data: `AppDbContext` com EF Core InMemory.
- Services: regras de negócio/acesso a dados por entidade.
- Controllers: endpoints REST com status codes, paginação e HATEOAS.
- Helpers: utilitários (ex.: links HATEOAS).

Boas práticas adotadas:
- CRUD completo para as 3 entidades.
- Paginação e HATEOAS no endpoint de listagem de `Moto`.
- Swagger/OpenAPI com descrições, modelos e exemplos de payloads.

## Instruções de execução da API
1) Pré-requisito: .NET 8 SDK instalado.
2) Executar:
   - dotnet run --project Sprint-4/Sprint-4.csproj
3) Swagger:
   - http://localhost:5000/swagger ou https://localhost:5001/swagger

## Exemplos de uso dos endpoints
- Moto
  - GET /api/moto?page=1&pageSize=10
  - GET /api/moto/1
  - POST /api/moto
  - PUT /api/moto/1
  - DELETE /api/moto/1
- Mecanico
  - GET /api/mecanico?page=1&pageSize=10
  - GET /api/mecanico/1
  - POST /api/mecanico
  - PUT /api/mecanico/1
  - DELETE /api/mecanico/1
- Depósito
  - GET /api/deposito?page=1&pageSize=10
  - GET /api/deposito/1
  - POST /api/deposito
  - PUT /api/deposito/1
  - DELETE /api/deposito/1

Observação: Exemplos de payloads estão no Swagger UI (Swashbuckle.AspNetCore.Filters).

## Comando para rodar os testes
- dotnet test Sprint-4/Sprint-4.csproj
