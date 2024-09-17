# Stage 1: Build the application
FROM node:16-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .

# Add build argument and set environment variable
ARG VITE_API_URL
ENV VITE_API_URL=$VITE_API_URL
# ENV VITE_API_URL=${VITE_API_URL}

# Build the application
RUN npm run build --verbose

# Stage 2: Serve the application with Nginx
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx/nginx.conf /etc/nginx/nginx.conf
