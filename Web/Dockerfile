# -------------- build stage ------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# restore depedency
COPY ./Web.csproj ./
RUN dotnet restore

# compile app
COPY . .
RUN dotnet publish -c Release -o /app --no-restore /p:UseAppHost=false

# ---------------- runtime stage -------------------
FROM nginx:alpine AS runtime
WORKDIR /app

COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/wwwroot/. ./wwwroot/