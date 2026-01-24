# -------- Build Stage --------
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy everything else and publish
COPY . .
RUN dotnet publish -c Release -o out

# -------- Runtime Stage --------
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/out .

# Expose port 80
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "ASPNetCore.dll"]
