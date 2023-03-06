FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/BorrowService.Api/BorrowService.Api.csproj", "src/BorrowService.Api/"]
COPY ["src/BorrowService.Borrowings/BorrowService.Borrowings.csproj", "src/BorrowService.Borrowings/"]
COPY ["src/BorrowService.Hangfire/BorrowService.Hangfire.csproj", "src/BorrowService.Hangfire/"]
COPY ["src/BorrowService.RabbitMq/BorrowService.RabbitMq.csproj", "src/BorrowService.RabbitMq/"]
RUN dotnet restore "src/BorrowService.Api/BorrowService.Api.csproj"
COPY . .
WORKDIR "/src/src/BorrowService.Api"
RUN dotnet build "BorrowService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BorrowService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BorrowService.Api.dll"]
