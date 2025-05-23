FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
# Expor as portas que Kestrel vai usar (conforme launchSettings.json ou .env)
EXPOSE 5039
EXPOSE 7189

# Configurar Kestrel para ouvir em ambas as portas
ENV ASPNETCORE_URLS="http://+:5039;https://+:7189"

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
# USER appuser # Será definido no estágio final

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG configuration=Release
# Argumento para a senha do certificado, que virá do docker-compose (do .env)
ARG CERT_PASSWORD_ARG

WORKDIR /src
COPY ["src/Adapters/Driving/StackFood.API/StackFood.API.csproj", "src/Adapters/Driving/StackFood.API/"]
RUN dotnet restore "src/Adapters/Driving/StackFood.API/StackFood.API.csproj"
COPY . .

# Gerar o certificado de desenvolvimento dentro do estágio de build
RUN mkdir -p /https_build
RUN dotnet dev-certs https -ep /https_build/aspnetapp.pfx -p "${CERT_PASSWORD_ARG}"

WORKDIR "/src/src/Adapters/Driving/StackFood.API"
RUN dotnet build "StackFood.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
# O certificado já foi gerado no estágio 'build', não precisa ser gerado aqui novamente.
RUN dotnet publish "StackFood.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

USER root
RUN mkdir -p /https # Cria o diretório para o certificado no estágio final
# Copia o certificado gerado no estágio 'build' para o estágio 'final'
COPY --from=build /https_build/aspnetapp.pfx /https/aspnetapp.pfx
RUN chown appuser:appuser /https/aspnetapp.pfx && chmod 600 /https/aspnetapp.pfx
RUN apt-get update && apt-get install -y curl

USER appuser

# Configura Kestrel para usar o certificado.
# A senha (ASPNETCORE_Kestrel__Certificates__Default__Password) será fornecida em tempo de execução pelo docker-compose.
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StackFood.API.dll"]