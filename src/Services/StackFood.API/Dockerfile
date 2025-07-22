# --- Base runtime image ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Expor as portas que Kestrel vai usar
EXPOSE 5039
EXPOSE 7189

# Configurar Kestrel para ouvir em ambas as portas
ENV ASPNETCORE_URLS="http://+:5039;https://+:7189"

# Criar usuário não-root para rodar a aplicação
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app

# --- Build image ---
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
ARG CERT_PASSWORD_ARG

WORKDIR /src

# Copiar csproj e restaurar dependências
COPY ["src/Adapters/Driving/StackFood.API/StackFood.API.csproj", "src/Adapters/Driving/StackFood.API/"]
RUN dotnet restore "src/Adapters/Driving/StackFood.API/StackFood.API.csproj"

# Copiar todo o código fonte
COPY . .

# Gerar certificado de desenvolvimento
RUN mkdir -p /https_build \
    && dotnet dev-certs https -ep /https_build/aspnetapp.pfx -p "${CERT_PASSWORD_ARG}"

# Build da aplicação
WORKDIR "/src/src/Adapters/Driving/StackFood.API"
RUN dotnet build "StackFood.API.csproj" -c $configuration -o /app/build

# --- Publish image ---
FROM build AS publish
ARG configuration=Release
RUN dotnet publish "StackFood.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# --- Final image ---
FROM base AS final
WORKDIR /app

# Instalar dependências necessárias
USER root
RUN apt-get update && apt-get install -y curl

# Copiar certificado gerado no build
RUN mkdir -p /https
COPY --from=build /https_build/aspnetapp.pfx /https/aspnetapp.pfx
RUN chown appuser:appuser /https/aspnetapp.pfx && chmod 600 /https/aspnetapp.pfx

# Configurar Kestrel para usar o certificado
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

# Copiar arquivos publicados
COPY --from=publish /app/publish .

USER appuser

ENTRYPOINT ["dotnet", "StackFood.API.dll"]