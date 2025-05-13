# StackFood API - Development Environment

This repository contains the StackFood API project, a .NET 7 application using PostgreSQL for data storage. Follow this guide to set up and run the development environment using Docker Compose.

## Prerequisites

Ensure the following are installed:

- [Docker](https://www.docker.com/products/docker-desktop/) (20.10.0+)
- [Docker Compose](https://docs.docker.com/compose/install/) (v2.0.0+)
- Git

## Quick Start

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/stackfood-api.git
cd stackfood-api

# Copy and configure the environment file
cp .env.example .env
```

Edit the `.env` file to customize settings, especially the database password.

### 2. Start the Application

```bash
# Start services
docker-compose up -d
```

- API: [http://localhost:7189](http://localhost:7189)
- Swagger UI: [http://localhost:7189/swagger/index.html](http://localhost:7189/swagger/index.html)

## Environment Configuration

The `.env` file contains key configuration variables:

| Variable            | Description              | Default         |
| ------------------- | ------------------------ | --------------- |
| API_VERSION         | API image version        | 1.0.0           |
| API_PORT            | API host port            | 7189            |
| ENVIRONMENT         | ASP.NET Core environment | Development     |
| BUILD_CONFIGURATION | Build configuration      | Debug           |
| POSTGRES_DB         | PostgreSQL database name | stackfood       |
| POSTGRES_USER       | PostgreSQL username      | postgres        |
| POSTGRES_PASSWORD   | PostgreSQL password      | StrongP@ssw0rd! |
| POSTGRES_PORT       | PostgreSQL host port     | 5432            |

## Project Structure

```
├── src/
│   ├── Adapters/
│   │   ├── Driven/
│   │   └── Driving/
│   │       └── StackFood.API/
│   │           └── Dockerfile
│   └── Core/
├── docker-compose.yml
├── .env.example
└── README.md
```

## Available Services

| Service       | Description                | Port |
| ------------- | -------------------------- | ---- |
| stackfood.api | .NET 7 API with Swagger UI | 7189 |
| postgres      | PostgreSQL 15.3 database   | 5432 |

## Development Workflow

1. Modify the code.
2. The API container auto-reloads changes.
3. Test via Swagger UI or an API client.
4. Commit and push updates.

## Database Management

### Connecting to PostgreSQL

- **Using Docker**:

  ```bash
  docker exec -it stackfood-db psql -U postgres -d stackfood
  ```

- **Using a PostgreSQL Client**:
  - **Host**: `localhost`
  - **Port**: `5432`
  - **Database**: `stackfood`
  - **Username**: `postgres`
  - **Password**: Refer to `POSTGRES_PASSWORD` in `.env`.

### Creating Backups

```bash
docker exec stackfood-db pg_dump -U postgres -d stackfood > backup_$(date +%Y%m%d_%H%M%S).sql
```

Backups are stored in the `backup_data` volume.

## Troubleshooting

### API Not Starting

Check logs for errors:

```bash
docker-compose logs stackfood.api
```

### Database Connection Issues

Ensure the database service is running:

```bash
docker-compose ps postgres
```

Verify `.env` credentials.

### Resetting the Environment

To reset and remove all data:

```bash
docker-compose down -v
docker-compose up -d
```

This recreates containers and volumes for a fresh start.
