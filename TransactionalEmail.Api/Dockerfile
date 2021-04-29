FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TransactionalEmail.Api/TransactionalEmail.Api.csproj", "TransactionalEmail.Api/"]

RUN dotnet restore "TransactionalEmail.Api/TransactionalEmail.Api.csproj"
COPY . .
WORKDIR "/src/TransactionalEmail.Api"
RUN dotnet build "TransactionalEmail.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TransactionalEmail.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransactionalEmail.Api.dll"]
