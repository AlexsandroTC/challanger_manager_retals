Comandos EF:

Criar a migrations intial:
dotnet ef migrations add InitialCreate -p manager_retals.Infrastructure  -s manager_retals.Api

Update do bando de dados:
dotnet ef database update -p manager_retals.Infrastructure -s manager_retals.Api
