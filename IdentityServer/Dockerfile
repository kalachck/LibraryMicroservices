FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /src
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
<<<<<<< Updated upstream

RUN dotnet restore "src/IdentityService.Api/IdentityService.Api.csproj"
=======
COPY IdentityService.Api/IdentityService.Api.csproj ./src/
COPY IdentityService.BusinessLogic/IdentityService.BusinessLogic.csproj ./src/
COPY IdentityService.DataAccess/IdentityService.DataAccess.csproj ./src/

RUN dotnet restore
>>>>>>> Stashed changes
COPY . .
WORKDIR /src
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /src
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.Api.dll"]
