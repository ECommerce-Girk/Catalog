# Catalog Microservice

This is a standalone microservice for managing product information in an e-commerce system.

## Architecture

The service follows a layered architecture pattern with three main layers:

1. **Presentation Layer** (`Catalog.Api`)
   - ASP.NET Core Web API controllers
   - REST endpoints for product operations
   - Authentication and authorization middleware

2. **Application Layer** (`Catalog.Core`)
   - Domain models and business logic
   - Commands and query objects
   - Interfaces for repositories and services

3. **Infrastructure Layer** (`Catalog.Infra`)
   - Entity Framework Core implementation
   - Database context and repository patterns
   - Implementation of authentication services

## Features

- Create, read, update, and delete products
- JWT-based authentication
- PostgreSQL database integration
- RESTful API endpoints

## Getting Started

1. Update connection string in `appsettings.Development.json`
2. Run migrations: `dotnet ef database update`
3. Start the service: `dotnet run --project Catalog.Api`

## Endpoints

- `POST /products` - Create a new product
- `POST /login` - Authenticate and get JWT token