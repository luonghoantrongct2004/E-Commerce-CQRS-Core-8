# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_NOLOGO=1
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

# Copy the published output from a previous build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/E.API/E.API.csproj", "src/E.API/"]
COPY ["src/E.Application/E.Application.csproj", "src/E.Application/"]
COPY ["src/E.Domain/E.Domain.csproj", "src/E.Domain/"]
COPY ["src/E.Infrastructure/E.Infrastructure.csproj", "src/E.Infrastructure/"]
RUN dotnet restore "src/E.API/E.API.csproj"
COPY . .
WORKDIR "/src/src/E.API"
RUN dotnet build "E.API.csproj" --configuration Release --no-restore --nologo --output /app/build 

# Use the base image to run the app
FROM base AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "E.API.dll"]
