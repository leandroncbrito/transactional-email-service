FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TransactionalEmail.Consumer/TransactionalEmail.Consumer.csproj", "TransactionalEmail.Consumer/"]
RUN dotnet restore "TransactionalEmail.Consumer/TransactionalEmail.Consumer.csproj"
COPY . .
WORKDIR "/src/TransactionalEmail.Consumer"
RUN dotnet build "TransactionalEmail.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TransactionalEmail.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransactionalEmail.Consumer.dll"]
