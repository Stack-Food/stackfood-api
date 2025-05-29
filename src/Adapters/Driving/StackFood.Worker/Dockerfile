FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Adapters/Driving/StackFood.Worker/StackFood.Worker.csproj", "src/Adapters/Driving/StackFood.Worker/"]
RUN dotnet restore "src/Adapters/Driving/StackFood.Worker/StackFood.Worker.csproj"
COPY . .
WORKDIR "/src/src/Adapters/Driving/StackFood.Worker"
RUN dotnet build "StackFood.Worker.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "StackFood.Worker.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StackFood.Worker.dll"]
