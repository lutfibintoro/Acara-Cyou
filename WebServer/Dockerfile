# -------------- build stage ------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# restore depedency
COPY ./WebServer.csproj ./
RUN dotnet restore

# compile app
COPY . .
RUN dotnet ef database update
RUN dotnet publish -c Release -o /app --no-restore /p:UseAppHost=false

# ---------------- runtime stage -------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/. ./

ENTRYPOINT ["dotnet", "WebServer.dll"]