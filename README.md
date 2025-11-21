# 🦈 FinShark

Uma API RESTful completa para gerenciamento de portfólio de ações desenvolvida com **ASP.NET Core 8.0**, Entity Framework Core e SQL Server. O projeto permite que usuários gerenciem suas ações, criem comentários sobre empresas e administrem seus portfólios de investimentos.

## 📋 Índice

- [Características](#características)
- [Pré-requisitos](#pré-requisitos)
- [Instalação](#instalação)
- [Configuração](#configuração)
- [Uso](#uso)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [API Endpoints](#api-endpoints)
- [Modelos de Dados](#modelos-de-dados)
- [Tecnologias](#tecnologias)
- [Segurança](#segurança)
- [Licença](#licença)

## ✨ Características

- ✅ **CRUD Completo de Ações**: Criar, ler, atualizar e deletar informações de ações
- ✅ **Gerenciamento de Portfólio**: Adicionar ações ao portfólio pessoal
- ✅ **Sistema de Comentários**: Comentar sobre ações e compartilhar análises
- ✅ **Usuários e Autenticação**: Sistema de usuários com Identity do ASP.NET Core (em desenvolvimento)
- ✅ **Documentação Interativa**: Swagger/OpenAPI UI integrada
- ✅ **Banco de Dados Relacional**: SQL Server com Entity Framework Core
- ✅ **Migrations Automáticas**: Controle de versão do banco de dados

## 🔧 Pré-requisitos

- **.NET 8.0 SDK** ou superior - [Download aqui](https://dotnet.microsoft.com/download)
- **SQL Server** (Local, Docker, ou Azure) - [Download aqui](https://www.microsoft.com/sql-server/sql-server-downloads)
- **Git** para controle de versão - [Download aqui](https://git-scm.com)

## 📥 Instalação

### 1. Clone o Repositório

```bash
git clone https://github.com/maxwellfarias/FinShark.git
cd FinShark
```

### 2. Restaure as Dependências

```bash
dotnet restore
```

## ⚙️ Configuração

### 1. Configure a Connection String

Este projeto utiliza **User Secrets** para armazenar credenciais de forma segura durante o desenvolvimento. Isso garante que informações sensíveis não sejam commitadas no repositório.

Execute o comando abaixo na raiz do projeto para adicionar sua connection string:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Data Source=SEU_SERVIDOR;Initial Catalog=fin_shark;User Id=SEU_USUARIO;Password=SUA_SENHA;TrustServerCertificate=true"
```

**Exemplos:**

Para **SQL Server Local**:
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Data Source=localhost;Initial Catalog=fin_shark;User Id=sa;Password=YourPassword123;TrustServerCertificate=true"
```

Para **SQL Server em Docker**:
```bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourPassword123' -p 1433:1433 -d mcr.microsoft.com/mssql/server:latest
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Data Source=localhost,1433;Initial Catalog=fin_shark;User Id=sa;Password=YourPassword123;TrustServerCertificate=true"
```

### 2. Crie o Banco de Dados

Execute as migrations para criar as tabelas no banco de dados:

```bash
dotnet ef database update
```

## 🚀 Uso

### Inicie a API

```bash
dotnet run
```

A aplicação será iniciada e abrirá automaticamente a página do **Swagger UI** no seu navegador padrão.

- **Swagger UI**: [https://localhost:7206/swagger](https://localhost:7206/swagger)
- **API Base URL**: `https://localhost:7206`
- **HTTP Alternativo**: `http://localhost:5037`

## 📁 Estrutura do Projeto

```
FinShark/
├── Controllers/
│   └── StockController.cs          # Endpoints para gerenciamento de ações
├── Models/
│   ├── Stock.cs                    # Modelo de Ação
│   ├── Comment.cs                  # Modelo de Comentário
│   ├── Portfolio.cs                # Modelo de Portfólio
│   └── AppUser.cs                  # Modelo de Usuário
├── Data/
│   ├── ApplicationDBContext.cs      # DbContext do Entity Framework
│   └── Migrations/                 # Scripts de migração do banco
├── Properties/
│   └── launchSettings.json          # Configurações de inicialização
├── Program.cs                       # Configuração e bootstrap da aplicação
├── appsettings.json                # Configurações da aplicação
└── FinShark.csproj                 # Definição do projeto
```

## 📡 API Endpoints

### Ações (Stocks)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/api/stock` | Obtém todas as ações |
| `GET` | `/api/stock/{id}` | Obtém uma ação específica |
| `POST` | `/api/stock` | Cria uma nova ação |
| `PUT` | `/api/stock/{id}` | Atualiza uma ação |
| `DELETE` | `/api/stock/{id}` | Deleta uma ação |

**Exemplo de Requisição:**

```bash
# Obter todas as ações
curl -X GET "https://localhost:7206/api/stock" \
  -H "Content-Type: application/json"

# Obter uma ação específica
curl -X GET "https://localhost:7206/api/stock/1" \
  -H "Content-Type: application/json"
```

## 📊 Modelos de Dados

### Stock (Ação)

```csharp
public class Stock
{
    public int Id { get; set; }
    public string Symbol { get; set; }           // Ex: "AAPL"
    public string CompanyName { get; set; }      // Ex: "Apple Inc"
    public decimal Purchase { get; set; }        // Preço de compra
    public decimal LastDiv { get; set; }         // Último dividendo
    public string Industry { get; set; }         // Setor
    public long MarketCap { get; set; }          // Capitalização de mercado
    
    public List<Comment> Comments { get; set; }
    public List<Portfolio> Portfolios { get; set; }
}
```

### Comment (Comentário)

```csharp
public class Comment
{
    public int? Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedOn { get; set; }
    
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public int? StockId { get; set; }
    public Stock? Stock { get; set; }
}
```

### Portfolio (Portfólio)

```csharp
public class Portfolio
{
    public int Id { get; set; }
    public string AppUserId { get; set; }
    public int StockId { get; set; }
    
    public AppUser AppUser { get; set; }
    public Stock Stock { get; set; }
}
```

### AppUser (Usuário)

```csharp
public class AppUser : IdentityUser
{
    public List<Portfolio> Portfolios { get; set; }
}
```

## 🛠️ Tecnologias

| Tecnologia | Versão | Descrição |
|------------|--------|-----------|
| **ASP.NET Core** | 8.0 | Framework web |
| **Entity Framework Core** | 8.0.11 | ORM para banco de dados |
| **SQL Server** | - | Banco de dados relacional |
| **Swagger/Swashbuckle** | 6.6.2 | Documentação de API |
| **Microsoft Identity** | 8.0 | Autenticação e autorização |

## 🔒 Segurança

### User Secrets
Este projeto usa **User Secrets** para armazenar informações sensíveis de forma segura durante o desenvolvimento:

- As credenciais do banco de dados são armazenadas localmente em `~/.microsoft/usersecrets/`
- O arquivo `appsettings.Development.json` está no `.gitignore`
- A connection string nunca é commitada no repositório

### Para Produção

Para ambientes de produção, configure as credenciais usando:

- **Azure Key Vault** - Recomendado para aplicações na Azure
- **AWS Secrets Manager** - Para aplicações na AWS
- **Variáveis de Ambiente** - Configuradas no servidor de deploy
- **Docker Secrets** - Se usando Docker Swarm

⚠️ **NUNCA** commite credenciais, senhas ou informações sensíveis no repositório, mesmo que seja público!

## 📝 Próximas Funcionalidades

- [ ] Endpoints completos de CRUD para Comentários
- [ ] Endpoints completos de CRUD para Portfólio
- [ ] Autenticação e Autorização com JWT
- [ ] Paginação e filtros avançados
- [ ] Validação de dados com Fluent Validation
- [ ] Testes unitários e de integração
- [ ] CI/CD Pipeline

## 🤝 Contribuições

Contribuições são bem-vindas! Para contribuir:

1. Faça um Fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está licenciado sob a Licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.

## 👨‍💻 Autor

**Maxwell Farias**

- GitHub: [@maxwellfarias](https://github.com/maxwellfarias)
- Email: [seu-email@exemplo.com]

## 📚 Recursos Adicionais

- [Documentação do ASP.NET Core](https://learn.microsoft.com/pt-br/aspnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/)
- [Swagger/OpenAPI](https://swagger.io/)
- [SQL Server Documentation](https://learn.microsoft.com/pt-br/sql/sql-server/)

---

**⭐ Se este projeto foi útil para você, deixe uma estrela no GitHub!**
