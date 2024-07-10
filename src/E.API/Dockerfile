# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files and restore dependencies
COPY *.csproj ./
RUN dotnet restore "E.API.csproj"

# Copy the remaining source code and build the application
COPY . .
WORKDIR "/src"
RUN dotnet build "./E.API.csproj" -c Release -o /app/build

# Publish the application
RUN dotnet publish "./E.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Create the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 8080 and specify the entry point
EXPOSE 8080
ENTRYPOINT ["dotnet", "E.API.dll"]
