FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TransactionalEmail.Cli/TransactionalEmail.Cli.csproj", "TransactionalEmail.Cli/"]

RUN dotnet restore "TransactionalEmail.Cli/TransactionalEmail.Cli.csproj"
COPY . .
WORKDIR "/src/TransactionalEmail.Cli"
RUN dotnet build "TransactionalEmail.Cli.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TransactionalEmail.Cli.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransactionalEmail.Cli.dll"]
