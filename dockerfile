# Imagen base con el SDK de .NET para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Establecer el directorio de trabajo en la carpeta de la solución
WORKDIR /app

# Copiar los archivos de proyecto y restaurar las dependencias
COPY *.sln .
COPY BusinessLogic/*.csproj ./BusinessLogic/
COPY Core/*.csproj ./Core/
COPY WebApi/*.csproj ./WebApi/
# Agregar más líneas si tienes más proyectos
RUN dotnet restore

# Copiar todo el contenido al directorio de trabajo
COPY . .

# Compilar la aplicación
RUN dotnet build -c Release

# Publicar la aplicación
RUN dotnet publish -c Release -o out

# Imagen base con el tiempo de ejecución de .NET
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

# Establecer el directorio de trabajo en la carpeta de publicación
WORKDIR /app

# Copiar los archivos publicados desde la imagen de compilación
COPY --from=build /app/out ./

# Exponer el puerto en el que se ejecutará la aplicación
EXPOSE 80
