# -----------------------------------------------------------------------------
# Stage 1: JavaScript Application Build (js-builder)
# Builds the front-end application located at WebApp/weather-check
# -----------------------------------------------------------------------------
FROM node:20-slim AS js-builder
WORKDIR /app

# Define the location of the JavaScript project relative to the Docker context root (solution root)
# Ensure this path is correct relative to where you run 'docker build'
ARG JS_PROJECT_DIR=WebApp/weather-check
ARG BUILD_OUTPUT_DIR=dist

# 1. Copy package files and install dependencies
COPY ${JS_PROJECT_DIR}/package*.json ./

# Install dependencies
RUN npm install --force

# 2. Copy the remaining source code and run the build
COPY ${JS_PROJECT_DIR} .

# Run the front-end build script (e.g., ng build --prod).
# This assumes 'npm run build' generates static files in the 'dist' folder.
RUN npm run build
# -----------------------------------------------------------------------------
# Stage 1: Build Stage
# Uses the full .NET SDK to compile and publish the application.
# -----------------------------------------------------------------------------
# Using mcr.microsoft.com/dotnet/sdk:10.0 as requested. Adjust the version 
# (e.g., 8.0) if 10.0 is not yet available or if you need a specific LTS version.
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the project file(s) and restore dependencies. 
# This leverages Docker layer caching: if the project file doesn't change, 
# 'dotnet restore' won't rerun, speeding up subsequent builds.
COPY ["CloudComputingAPI/CloudComputingAPI.csproj", "CloudComputingAPI/"] 
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"] 
RUN dotnet restore "CloudComputingAPI/CloudComputingAPI.csproj"

# Copy the remaining source code
COPY . .

# Change to the working directory for the project
WORKDIR /src/CloudComputingAPI

# Publish the application to /app/publish. 
# The -c Release ensures it's optimized for production.
RUN dotnet publish "CloudComputingAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false
COPY --from=js-builder /app/dist /app/publish/wwwroot
# -----------------------------------------------------------------------------
# Stage 2: Final (Runtime) Stage
# Uses the much smaller ASP.NET runtime image to run the published files.
# -----------------------------------------------------------------------------
# Using mcr.microsoft.com/dotnet/aspnet:10.0 as requested.
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Copy the published output from the 'build' stage
COPY --from=build /app/publish .

# The default ASP.NET Core port is 8080 when running in a container, 
# but we explicitly expose 8080 here.
EXPOSE 80

# Define the entry point to run the published assembly
ENTRYPOINT ["dotnet", "CloudComputingAPI.dll"]