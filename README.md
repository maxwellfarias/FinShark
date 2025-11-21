# FinShark

API de gerenciamento financeiro desenvolvida com ASP.NET Core.

## Configuração do Projeto

### Pré-requisitos

- .NET 8.0 SDK ou superior
- SQL Server (local ou remoto)

### Configuração do Banco de Dados

Este projeto utiliza **User Secrets** para armazenar informações sensíveis de conexão com o banco de dados de forma segura durante o desenvolvimento.

#### 1. Configure a Connection String

Execute o seguinte comando na raiz do projeto para adicionar sua connection string aos User Secrets:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Data Source=SEU_SERVIDOR;Initial Catalog=fin_shark;User Id=SEU_USUARIO;Password=SUA_SENHA;TrustServerCertificate=true"
```

**Exemplo:**
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Data Source=localhost;Initial Catalog=fin_shark;User Id=sa;Password=MinhaSenh@123;TrustServerCertificate=true"
```

#### 2. Execute as Migrations

Após configurar a connection string, execute as migrations para criar o banco de dados:

```bash
dotnet ef database update
```

### Executando o Projeto

```bash
dotnet run
```

A API estará disponível em:
- HTTPS: https://localhost:5001
- Swagger UI: https://localhost:5001/swagger

## Estrutura do Projeto

- **Controllers/**: Controladores da API
- **Data/**: Contexto do Entity Framework
- **Models/**: Modelos de dados (Stock, Comment, Portfolio, AppUser)
- **Migrations/**: Migrations do Entity Framework

## Tecnologias Utilizadas

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger/OpenAPI

## Segurança

⚠️ **IMPORTANTE**: Nunca commite informações sensíveis como senhas ou connection strings no repositório. Este projeto utiliza User Secrets para desenvolvimento local e o arquivo `appsettings.Development.json` está no `.gitignore`.

Para produção, utilize variáveis de ambiente ou serviços de configuração seguros como Azure Key Vault, AWS Secrets Manager, etc.
