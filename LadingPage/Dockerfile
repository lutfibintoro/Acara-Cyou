# -------------- build stage ------------------
FROM node:22-alpine AS build
WORKDIR /src

# restore depedency
COPY package*.json ./
RUN npm install

# compile app
COPY . .
RUN npm run build

# ---------------- runtime stage -------------------
FROM nginx:alpine AS runtime
WORKDIR /app

COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /src/dist/. ./