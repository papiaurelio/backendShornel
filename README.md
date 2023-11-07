# backendShornel


## comandos

---Backend| asp

(imagen sql server docker)
docker pull mcr.microsoft.com/mssql/server

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Aurelio444*" -p 1433:1433 -d mcr.microsoft.com/mssql/server
docker container list


docker container list -a
docker start {nombreConteiner}

https://www.nuget.org/packages/dotnet-ef/8.0.0-rc.2.23480.1
dotnet --list-runtimes
dotnet tool install --global dotnet-ef --version 7.0.9

dotnet tool uninstall dotnet-ef --global


----
dotnet ef migrations add MigracionInicial -p BusinessLogic -s WebApi -o Data/Migrations

dotnet --list-runtimes


http://localhost:4204/api/producto?pageIndex=4&pageSize=2
