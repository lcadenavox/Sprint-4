# ENTREGA - 3º SPRINT

## Nomes dos integrantes
- [Adicione aqui os nomes dos integrantes do grupo]

## Justificativa da Arquitetura
A arquitetura foi baseada em boas práticas REST, utilizando .NET 8 Web API. O projeto está organizado em camadas:
- **Models**: Definição das entidades principais (`Moto`, `Mecanico`, `Deposito`).
- **Data**: Contexto do Entity Framework para persistência em memória.
- **Services**: Lógica de negócio e acesso aos dados.
- **Controllers**: Endpoints RESTful, seguindo boas práticas (status codes, paginação, HATEOAS).
- **Helpers**: Utilitários como links HATEOAS.

Essa separação facilita manutenção, testes e escalabilidade.

## Instruções de Execução da API
1. Certifique-se de ter o [.NET 8 SDK](https://dotnet.microsoft.com/download) instalado.
2. No terminal, navegue até a pasta do projeto e execute:
   ```bash
   dotnet run --project Sprint-4/Sprint-4.csproj
   ```
3. Acesse a documentação Swagger em: [https://localhost:5001/swagger](https://localhost:5001/swagger) ou [http://localhost:5000/swagger](http://localhost:5000/swagger)

## Exemplos de Uso dos Endpoints
### Moto
- **Listar motos (com paginação):**
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
- **Listar mecânicos:**
  ```http
  GET /api/mecanico
  ```
- **Obter mecânico por ID:**
  ```http
  GET /api/mecanico/1
  ```
- **Criar mecânico:**
  ```http
  POST /api/mecanico
  Content-Type: application/json
  {
    "nome": "João Silva",
    "especialidade": "Motor"
  }
  ```

### Depósito
- **Listar depósitos:**
  ```http
  GET /api/deposito
  ```
- **Obter depósito por ID:**
  ```http
  GET /api/deposito/1
  ```
- **Criar depósito:**
  ```http
  POST /api/deposito
  Content-Type: application/json
  {
    "nome": "Depósito Central",
    "endereco": "Rua das Flores, 123"
  }
  ```

## Comando para rodar os testes
Caso existam testes automatizados, utilize:
```bash
dotnet test Sprint-4/Sprint-4.csproj
```

Se precisar de exemplos de testes, posso ajudar a criar!
