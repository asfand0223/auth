# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY Auth.sln ./
COPY Auth/Auth.csproj ./Auth/Auth.csproj
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o out

# Production
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS production
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 5000
ENTRYPOINT ["dotnet", "Auth.dll"]
