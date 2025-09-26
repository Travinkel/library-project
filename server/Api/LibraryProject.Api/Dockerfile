# Use the .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# IMPORTANT: This Dockerfile expects the build context to be the 'server' directory.
# See .github/workflows/docker.yml where context is set to ./server and this file is specified via 'file'.

# Copy project files and restore
COPY Api/LibraryProject.Api/LibraryProject.Api.csproj Api/LibraryProject.Api/
COPY DataAccess/LibraryProject.DataAccess/LibraryProject.DataAccess.csproj DataAccess/LibraryProject.DataAccess/
RUN dotnet restore Api/LibraryProject.Api/LibraryProject.Api.csproj

# Copy source code
COPY Api/LibraryProject.Api/ Api/LibraryProject.Api/
COPY DataAccess/LibraryProject.DataAccess/ DataAccess/LibraryProject.DataAccess/

WORKDIR /src/Api/LibraryProject.Api
RUN dotnet publish LibraryProject.Api.csproj -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LibraryProject.Api.dll"]
