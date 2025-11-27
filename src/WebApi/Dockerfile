# Base ASP.NET Runtime (produção)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# SDK para build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar os arquivos de projeto (csproj)
COPY src/Estudos.Microsservices.Contratos/Estudos.Microsservices.Contratos.csproj Estudos.Microsservices.Contratos/
COPY src/WebApi/WebApi.csproj WebApi/

# Restore
RUN dotnet restore WebApi/WebApi.csproj

# Copiar tudo
COPY src/ .

# Build
WORKDIR /src/WebApi
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publicar
FROM build AS publish
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
