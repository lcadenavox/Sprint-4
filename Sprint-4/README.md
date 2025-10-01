# ENTREGA - 3� SPRINT

## Nomes dos integrantes
- [Integrante 1]
- [Integrante 2]
- [Integrante 3]

## Justificativa do Dom�nio
Optamos por um dom�nio de gest�o de manuten��o automotiva com tr�s entidades:
- Moto: item de manuten��o e servi�o.
- Mecanico: respons�vel pela execu��o dos servi�os.
- Deposito: local de armazenamento/apoio para pe�as e log�stica.
Esse dom�nio � simples, familiar e cobre casos t�picos de CRUD, al�m de permitir demonstrar pagina��o, HATEOAS e documenta��o Swagger com clareza.

## Justificativa da Arquitetura
API RESTful em .NET 8 (Web API), com camadas:
- Models: `Moto`, `Mecanico`, `Deposito`.
- Data: `AppDbContext` com EF Core InMemory.
- Services: regras de neg�cio/acesso a dados por entidade.
- Controllers: endpoints REST com status codes, pagina��o e HATEOAS.
- Helpers: utilit�rios (ex.: links HATEOAS).

Boas pr�ticas adotadas:
- CRUD completo para as 3 entidades.
- Pagina��o e HATEOAS no endpoint de listagem de `Moto`.
- Swagger/OpenAPI com descri��es, modelos e exemplos de payloads.

## Instru��es de execu��o da API
1) Pr�-requisito: .NET 8 SDK instalado.
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
- Dep�sito
  - GET /api/deposito?page=1&pageSize=10
  - GET /api/deposito/1
  - POST /api/deposito
  - PUT /api/deposito/1
  - DELETE /api/deposito/1

Observa��o: Exemplos de payloads est�o no Swagger UI (Swashbuckle.AspNetCore.Filters).

## Comando para rodar os testes
- dotnet test Sprint-4/Sprint-4.csproj
