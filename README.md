# Manager Rentals - Teste Técnico .NET


Sistema de gerenciamento de locação de motocicletas, desenvolvido como teste técnico para vaga de Desenvolvedor .NET Pleno.

O projeto demonstra conceitos de Clean Architecture, Strategy Pattern, Dependency Injection, mensageria via RabbitMQ e Testes Unitários.
## 📦 Funcionalidades

- Criação de locação de motos com diferentes planos (7, 15, 30, 45, 50 dias).
- Cálculo automático do preço total do aluguel.
- Cálculo de devolução antecipada ou atrasada com penalidades.
- Validação de motorista e motocicleta antes da locação.
- Publicação de eventos de locação via RabbitMQ.
- Testes unitários cobrindo casos de sucesso e falhas.



## Stack utilizada

**Back-end:** .NET 8 / C# 12

**Pattern:**  Repository Pattern, Dependency Injection, Strategy Pattern, CQRS (Command Handler e Query Handlers), Clean Architecture / DDD

**ORM:** Entity Framework

**Banco de dados:** PostgreSQL (via Docker Compose)

**Messageria:** RabbitMQ (via Docker Compose)

**Unit Test:** xUnit, Moq, FluentAssertions 

## Demonstração

### 🏗 Estrutura do Projeto

```
manager_retals/
│
├─ manager_retals.Api/              # API
├─ manager_retals.Core/             # Domínio e regras
│  ├─ Commands/                     # Handlers e Commands
│  │  ├─ Driver/
│  │  │  ├─ CreateDriverCommand.cs
│  │  │  └─ CreateDriverHandler.cs
│  │  └─ Motorcycle/ Rental/ ...
│  ├─ Entities/                     # Entidades do domínio
│  ├─ Enums/                        # Enumeradores
│  ├─ Exceptions/                   # Exceções customizadas
│  ├─ Queries/                       # Queries de leitura
│  ├─ Repositories/                  # Interfaces de repositório
│  ├─ Services/                      # Serviços de cálculo
│  └─ Strategies/                    # Strategy Pattern
│     ├─ RentalPlanCalculator/
│     └─ RentalReturn/
│
├─ manager_retals.Infrastructure/     # Implementação de repositórios
├─ manager_retals.Infrastructure.RabbitMQ/  # Publicação de eventos
├─ manager_retals.Unit_Test/          # Testes unitários
└─ docker-compose.yml                 # PostgreSQL + RabbitMQ
```


## 🐳 Docker Compose

O projeto utiliza PostgreSQL e RabbitMQ via Docker Compose para simular o ambiente de produção:
```yaml
services:
  postgres:
    image: postgres:16-alpine
    container_name: db_postgres
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
      POSTGRES_DB: manager_rentals
    ports:
      - "5432:5432"
    volumes:
      - pgdata:/var/lib/postgresql/data

  rabbitmq:
    image: rabbitmq:3-management
    container_name: rabbitmq
    ports:
      - "5672:5672"
      - "15672:15672"
    environment:
      RABBITMQ_DEFAULT_USER: guest
      RABBITMQ_DEFAULT_PASS: guest
```

Para iniciar os containers de dentro da pasta raiz do projeto, execultar o command abaixo:

```bash
docker-compose up -d
```

## 🛠 Como Rodar

### 1. Clone o repositório:

    git clone https://github.com/seu-usuario/manager-rentals.git
    cd manager-rentals


Execute os containers do Docker:

    docker-compose up -d


Restaure pacotes NuGet:

    dotnet restore


Execute a aplicação:

    dotnet run --project src/manager_retals.Api


Execute os testes unitários:

    dotnet test


Rode o comando do EF para criar as tabelas no banco de dados:
    
    cd src/manager_retals.Api
    dotnet ef migrations add InitialCreate -p manager_retals.Infrastructure  -s manager_retals.Api
    dotnet ef database update -p manager_retals.Infrastructure -s manager_retals.Api


## 🧩 Strategy Pattern

O cálculo de planos de locação e devolução foi abstraído em estratégias:
- Cada plano (7 dias, 15 dias, etc.) implementa IRentalPlanStrategy.
- RentalPlanCalculationServices seleciona a strategy correta de acordo com o plano.
- Permite adicionar novos planos sem modificar código existente.

## ✅ Testes Unitários

Cobrem os seguintes cenários:
- Criação de locação com dados válidos.
- Motorista inexistente ou com licença incompatível.
- Motocicleta inexistente.
- Cálculo de preço e penalidade em devolução antecipada ou atrasada.


## Documentação da API

Estã implementado o swagger como documentação da API.

    https://localhost:7001/swagger/index.html

## 🎯 Objetivo

Demonstrar conhecimento em C#, .NET 8, Clean Architecture e Patterns.
Garantir qualidade de código com testes unitários.
Mostrar boa organização e escalabilidade do projeto.