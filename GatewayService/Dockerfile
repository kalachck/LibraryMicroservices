FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/GatewayService.Api/GatewayService.Api.csproj", "src/GatewayService.Api/"]
RUN dotnet restore "src/GatewayService.Api/GatewayService.Api.csproj"
COPY . .
WORKDIR "/src/src/GatewayService.Api"
RUN dotnet build "GatewayService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GatewayService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GatewayService.Api.dll"]
