FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/LibraryService.Api/LibrarySevice.Api.csproj", "src/LibraryService.Api/"]
COPY ["src/LibraryService.BussinesLogic/LibrarySevice.BussinesLogic.csproj", "src/LibraryService.BussinesLogic/"]
COPY ["src/LibraryService.DataAccess/LibrarySevice.DataAccess.csproj", "src/LibraryService.DataAccess/"]
RUN dotnet restore "src/LibraryService.Api/LibrarySevice.Api.csproj"
COPY . .
WORKDIR "/src/src/LibraryService.Api"
RUN dotnet build "LibrarySevice.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LibrarySevice.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibrarySevice.Api.dll"]
