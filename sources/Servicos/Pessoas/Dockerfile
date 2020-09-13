FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /app

RUN mkdir -p /usr/local/share/dotnet/sdk/NuGetFallbackFolder

# Copy csproj and restore as distinct layers
COPY . ./
RUN dotnet restore 

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Debug -o /app/publish Pessoas.API/Pessoas.API.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS publish

WORKDIR /app

COPY --from=build-env /app/publish .
# RUN apt-get update

EXPOSE 5001

ENTRYPOINT ["dotnet", "Pessoas.API.dll"]