# Runtime base
FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
WORKDIR /app

# Build SDK
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar csproj dos dois projetos
COPY src/Estudos.Microsservices.Contratos/Estudos.Microsservices.Contratos.csproj Estudos.Microsservices.Contratos/
COPY src/Estudos.Microsservices.Consumer/Estudos.Microsservices.Consumer.csproj Estudos.Microsservices.Consumer/

# Restore
RUN dotnet restore Estudos.Microsservices.Consumer/Estudos.Microsservices.Consumer.csproj

# Copiar tudo
COPY src/ .

# Build
WORKDIR /src/Estudos.Microsservices.Consumer
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM mcr.microsoft.com/dotnet/runtime:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Estudos.Microsservices.Consumer.dll"]
