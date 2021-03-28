FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app/src
COPY "./ElasticSearch.API/ElasticSearch.API.csproj" ./
RUN dotnet restore "ElasticSearch.API.csproj"
COPY "./ElasticSearch.API/" ./
RUN dotnet build "ElasticSearch.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElasticSearch.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElasticSearch.API.dll"]