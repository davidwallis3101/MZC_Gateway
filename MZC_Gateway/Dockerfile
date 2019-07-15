FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-stretch AS build
WORKDIR /src
COPY ["MZC_Gateway/MZC_Gateway.csproj", "MZC_Gateway/"]
RUN dotnet restore "MZC_Gateway/MZC_Gateway.csproj"
COPY . .
WORKDIR "/src/MZC_Gateway"
RUN dotnet build "MZC_Gateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MZC_Gateway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MZC_Gateway.dll"]