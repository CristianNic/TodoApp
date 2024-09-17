# TodoApp

## Overview
This application consists of a MongoDB database, an ASP.NET API service, and a React web application. The services are defined in a `docker-compose.yml` file and can be built and run using Docker Compose.

## Getting Started
Clone the repository to your local machine and navigate to the project directory where the `docker-compose.yml` file is located.

## Run

```bash
docker-compose up
```

## To test the running services, run the following command:

```bash
sh test_services.sh
```

## Accessing the Services
1. Frontend is available at <http://localhost:3000/>
2. Backend Swagger is available at <http://localhost:5001/swagger/index.html>

## MongoDB
3. MongoDB can be accessed with Compass or by connecting to <http://localhost:27017/>
