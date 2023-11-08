# backendShornel


## comandos

## ---Backend| asp

# Comandos docker
__(imagen sql server docker)__
- docker pull mcr.microsoft.com/mssql/server 

- docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Aurelio444*" -p 1433:1433 -d mcr.microsoft.com/mssql/server
- docker container list
- docker container list -a
- docker start {nombreConteiner}
__##v2 Imagen docker sql__
- docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=tu_contraseña" -p 1433:1433 --name sql_server_container -d mcr.microsoft.com/mssql/server:2019-latest

## Comandos Dotnet
https://www.nuget.org/packages/dotnet-ef/8.0.0-rc.2.23480.1
dotnet --list-runtimes
dotnet tool install --global dotnet-ef --version 7.0.9

dotnet tool uninstall dotnet-ef --global
dotnet ef migrations add MigracionInicial -p BusinessLogic -s WebApi -o Data/Migrations
dotnet --list-runtimes

(Seguridad Identity)
dotnet ef migrations add SeguridadInicio -p BusinessLogic -s WebApi -o Identity/Migrations -c SeguridadDbContext

## Comandos linux
dotnet run -r linux-x64 (ejecutar proyecto linux)


## Links test app
http://localhost:4204/api/producto?pageIndex=4&pageSize=2



