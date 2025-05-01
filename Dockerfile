# STAGE 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy solution and restore
COPY *.sln .
COPY GuidoAloise/GuidoAloise.csproj GuidoAloise/
COPY GuidoAloise.Api/GuidoAloise.Api.csproj GuidoAloise.Api/

RUN dotnet restore

# Copy everything and build
COPY . .

RUN dotnet publish GuidoAloise.Api/GuidoAloise.Api.csproj -c Release -o /app/publish

# STAGE 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "GuidoAloise.Api.dll"]
