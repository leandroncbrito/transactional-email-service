FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TransactionalEmail.Tests/TransactionalEmail.Tests.csproj", "TransactionalEmail.Tests/"]

RUN dotnet restore "TransactionalEmail.Tests/TransactionalEmail.Tests.csproj"
COPY . .
WORKDIR "/src/TransactionalEmail.Tests"
RUN dotnet build "TransactionalEmail.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TransactionalEmail.Tests.csproj" -c Release -o /app/publish

FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "test", "TransactionalEmail.Tests.dll"]
