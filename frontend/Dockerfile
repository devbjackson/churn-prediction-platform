# Stage 1: Build the React application
FROM node:18 AS build

WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# Stage 2: Serve the built application with Nginx
FROM nginx:stable-alpine

COPY --from=build /app/build /usr/share/nginx/html

# When the container starts, nginx will run automatically
EXPOSE 80