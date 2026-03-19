Para migrar BD, en Infraestructure ejecutar:

dotnet ef migrations add InitialCreate --startup-project ../SchoolApp.Api
dotnet ef database update --startup-project ../SchoolApp.Api
