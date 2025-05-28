FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Adapters/Driving/Worker.PaymentStatus/StackFood.Worker.PaymentStatus.csproj", "src/Adapters/Driving/Worker.PaymentStatus/"]
RUN dotnet restore "src/Adapters/Driving/Worker.PaymentStatus/StackFood.Worker.PaymentStatus.csproj"
COPY . .
WORKDIR "/src/src/Adapters/Driving/Worker.PaymentStatus"
RUN dotnet build "StackFood.Worker.PaymentStatus.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "StackFood.Worker.PaymentStatus.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StackFood.Worker.PaymentStatus.dll"]
