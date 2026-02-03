# -------------------------------
# Build Stage
# -------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY PortfolioWebApi/*.csproj ./PortfolioWebApi/

WORKDIR /app/PortfolioWebApi
RUN dotnet restore

COPY PortfolioWebApi/. ./

RUN dotnet publish -c Release -o out

# -------------------------------
# Runtime Stage
# -------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/PortfolioWebApi/out ./

ENV ASPNETCORE_URLS=http://+:$PORT

EXPOSE 80

ENTRYPOINT ["dotnet", "PortfolioWebApi.dll"]
