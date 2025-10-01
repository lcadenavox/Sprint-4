# ENTREGA - 3� SPRINT

## Nomes dos integrantes
- [Adicione aqui os nomes dos integrantes do grupo]

## Justificativa da Arquitetura
A arquitetura foi baseada em boas pr�ticas REST, utilizando .NET 8 Web API. O projeto est� organizado em camadas:
- **Models**: Defini��o das entidades principais (`Moto`, `Mecanico`, `Deposito`).
- **Data**: Contexto do Entity Framework para persist�ncia em mem�ria.
- **Services**: L�gica de neg�cio e acesso aos dados.
- **Controllers**: Endpoints RESTful, seguindo boas pr�ticas (status codes, pagina��o, HATEOAS).
- **Helpers**: Utilit�rios como links HATEOAS.

Essa separa��o facilita manuten��o, testes e escalabilidade.

## Instru��es de Execu��o da API
1. Certifique-se de ter o [.NET 8 SDK](https://dotnet.microsoft.com/download) instalado.
2. No terminal, navegue at� a pasta do projeto e execute:
   ```bash
   dotnet run --project Sprint-4/Sprint-4.csproj
   ```
3. Acesse a documenta��o Swagger em: [https://localhost:5001/swagger](https://localhost:5001/swagger) ou [http://localhost:5000/swagger](http://localhost:5000/swagger)

## Exemplos de Uso dos Endpoints
### Moto
- **Listar motos (com pagina��o):**
  ```http
  GET /api/moto?page=1&pageSize=10
  ```
- **Obter moto por ID:**
  ```http
  GET /api/moto/1
  ```
- **Criar moto:**
  ```http
  POST /api/moto
  Content-Type: application/json
  {
    "marca": "Honda",
    "modelo": "CG 160",
    "ano": 2022
  }
  ```
- **Atualizar moto:**
  ```http
  PUT /api/moto/1
  Content-Type: application/json
  {
    "marca": "Yamaha",
    "modelo": "Factor",
    "ano": 2023
  }
  ```
- **Deletar moto:**
  ```http
  DELETE /api/moto/1
  ```

### Mecanico
- **Listar mec�nicos:**
  ```http
  GET /api/mecanico
  ```
- **Obter mec�nico por ID:**
  ```http
  GET /api/mecanico/1
  ```
- **Criar mec�nico:**
  ```http
  POST /api/mecanico
  Content-Type: application/json
  {
    "nome": "Jo�o Silva",
    "especialidade": "Motor"
  }
  ```

### Dep�sito
- **Listar dep�sitos:**
  ```http
  GET /api/deposito
  ```
- **Obter dep�sito por ID:**
  ```http
  GET /api/deposito/1
  ```
- **Criar dep�sito:**
  ```http
  POST /api/deposito
  Content-Type: application/json
  {
    "nome": "Dep�sito Central",
    "endereco": "Rua das Flores, 123"
  }
  ```

## Comando para rodar os testes
Caso existam testes automatizados, utilize:
```bash
dotnet test Sprint-4/Sprint-4.csproj
```

Se precisar de exemplos de testes, posso ajudar a criar!
