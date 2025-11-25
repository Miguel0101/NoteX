# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy source project
COPY ./src .

# Publish the project
RUN dotnet publish ./NoteX.API -c Release --os linux -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /app .
COPY --from=build /src/NoteX.API/certificate.pfx .

ENTRYPOINT [ "dotnet", "NoteX.API.dll" ]