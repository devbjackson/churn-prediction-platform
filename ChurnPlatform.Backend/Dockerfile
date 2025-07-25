# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies first
# This leverages Docker's layer caching
COPY *.csproj .
RUN dotnet restore

# Copy the rest of the source code and build the application
COPY . .
RUN dotnet publish "ChurnPlatform.Backend.csproj" -c Release -o /app/publish

# Stage 2: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# The entrypoint command to run the application
ENTRYPOINT ["dotnet", "ChurnPlatform.Backend.dll"]