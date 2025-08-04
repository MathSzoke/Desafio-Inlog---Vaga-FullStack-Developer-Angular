
# Projeto Backend - Documentação

## Visão Geral

Este documento descreve as tecnologias, arquitetura, padrões utilizados, versões das dependências e instruções de como iniciar o projeto backend fornecido.

## Tecnologias e Versões

O projeto utiliza as seguintes tecnologias principais:

### Plataforma
- **.NET 9.0**

*OBS: Reparei na descrição do desafio de que era permitido realizar a atividade com .NET 6 ou superior,
então escolhi seguir com uma tecnologia moderna e certamente eficiente para qualquer demanda dos dia de hoje. Uma das razões principais de ter escolhido o .NET 9 foi
pelo simples fato de que diante de diversos testes de benchmark, essa versão da plataforma .NET foi considerada a melhor em performance e desempenho, o que nos dá segurança
em escolhe-la.*

### Frameworks e Bibliotecas Principais
- **Entity Framework Core** (9.0.7)
- **Npgsql.EntityFrameworkCore.PostgreSQL** (9.0.4)
- **FluentValidation** (12.0.0)
- **Serilog** (4.3.0)
- **Scrutor** (6.1.0)
- **Microsoft.AspNetCore.SignalR** (1.2.0)
- **Swashbuckle.AspNetCore** (6.5.0)
- **AspNetCore.HealthChecks.NpgSql** (9.0.0)

### Ferramentas de Teste
- **xUnit** (2.9.3)
- **Shouldly** (4.3.0)

### Banco de Dados
- **PostgreSQL**

## Arquitetura e Padrões

### Estrutura da Solução

O projeto é organizado em camadas seguindo princípios de **Clean Architecture**:

- **WebApi** (Camada de apresentação)
- **Application** (Regras de negócio e lógica de aplicação)
- **Domain** (Entidades e domínio principal)
- **Infrastructure** (Infraestrutura do banco de dados, contexto do EF Core, migrações)
- **SharedKernel** (Compartilhamento de definições entre todas as camadas)

### Padrões Utilizados

- **Mediator** (Manipuladores para comandos, consultas e eventos de domínio)
- **Repository Pattern** (Acesso aos dados através do Entity Framework)
- **Dependency Injection** (Injeção de dependência organizada)
- **Unit of Work** (Utilizado implicitamente via EF Core)
- **CQRS** (Command Query Responsibility Segregation)
- **Domain Events** (Eventos de domínio para manipulação de ações de negócio)
- **Decorators** (Validação e logging de comandos e consultas)
- **FluentValidation** (Validações estruturadas e claras)

### Detalhes da Implementação

O projeto aplica boas práticas como:
- Centralização das versões dos pacotes NuGet no arquivo `Directory.Packages.props`
- Uso de `ImplicitUsings` e `Nullable Reference Types` para garantir segurança e clareza no código

## Como Executar o Projeto

### Pré-requisitos

- [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/) instalado e configurado

### Passos para execução

1. Clone o repositório e acesse a pasta:
```bash
git clone https://github.com/MathSzoke/Desafio-Inlog---Vaga-FullStack-Developer-Angular.git
cd back-end/Inlog.Desafio.Backend
```

2. Configure a conexão com o banco de dados PostgreSQL em `appsettings.json`:
```json
"ConnectionStrings": {
  "inlogDB": "Host=localhost;Port=5432;Database=inlog;Username=postgres;Password=postgres"
}
```

3. Execute as migrações do banco de dados:
```bash
dotnet ef database update --project Inlog.Desafio.Backend.Infra.Database
```

4. Execute o projeto WebApi:
```bash
dotnet run --project Inlog.Desafio.Backend.WebApi
```

A API estará disponível em `https://localhost:5000`.

### Acesso ao Swagger

Após iniciar o projeto, acesse o Swagger UI através da URL:
```
https://localhost:5000/swagger
```

## Testes

Para executar os testes unitários com xUnit:
```bash
dotnet test
```

## Estrutura do Projeto

```
Inlog.Desafio.Backend/
├── WebApi/
├── Application/
├── Domain/
├── Infrastructure/
└── Test/
└── SharedKernel/
```

## Observações Finais

Esta documentação fornece uma visão abrangente do projeto backend, permitindo que desenvolvedores entendam rapidamente a arquitetura, as tecnologias utilizadas e o processo necessário para execução e testes.

