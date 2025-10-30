# Gemini Code Understanding

## Project Overview

This project is a .NET 8 Web API service named `Cliently.BusinessInfo-Service`. It follows the principles of Clean Architecture, separating concerns into `Core`, `Application`, `Infrastructure`, and `Presentation` layers.

The service is designed to manage business-related information, including:
- **Businesses**: The core entity representing a business.
- **InfoTexts**: Simple text information associated with a business.
- **InfoLists**: Lists of items associated with a business.
- **DynamicItems**: Items within an `InfoList` that have dynamic properties stored as JSONB.

Key technologies and patterns used:
- **.NET 8**: The underlying framework.
- **ASP.NET Core**: For building the Web API.
- **Entity Framework Core**: As the Object-Relational Mapper (ORM).
- **PostgreSQL**: The target database, indicated by the `EFPostgre` repositories and context.
- **Clean Architecture**: The project is structured into four distinct layers.
- **CQRS (Command Query Responsibility Segregation)**: The `Application` layer uses the `MediatR` library to separate commands (writes) from queries (reads).
- **Repository Pattern**: Data access is abstracted through repository interfaces defined in the `Core` layer and implemented in the `Infrastructure` layer.
- **Swagger/OpenAPI**: For API documentation and testing.

## Building and Running

The project is a standard .NET solution.

### Building the project

To build the entire solution, run the following command from the root directory:

```bash
dotnet build Cliently.BusinessInfoService.sln
```

### Running the application

The entry point for the application is the `Presentation` project. To run the service:

```bash
dotnet run --project src/Presentation/Presentation.csproj
```

The service will start, and you can access the Swagger UI at the URL specified in the console output (usually `https://localhost:port/swagger`).

**TODO:** The `Program.cs` file currently contains boilerplate code. The database context, repositories, and MediatR handlers need to be registered in the dependency injection container for the application to be fully functional.

### Running tests

There are no test projects in the solution yet. Once tests are added, you can run them using:

```bash
dotnet test
```

## Development Conventions

### Code Style

The codebase follows standard C# and .NET conventions.

### Architecture

- **`Core`**: Contains the domain models (entities) and repository interfaces. This layer has no external dependencies.
- **`Application`**: Contains the business logic, implemented using CQRS. It defines commands, queries, and their handlers. It depends on the `Core` layer.
- **`Infrastructure`**: Implements the repository interfaces from the `Core` layer, providing data access using Entity Framework Core and PostgreSQL. It depends on the `Core` and `Application` layers.
- **`Presentation`**: The ASP.NET Core Web API project that exposes the application's functionality via HTTP endpoints. It depends on all other layers.

### Data Access

- Data access is handled by repositories that implement interfaces from the `Core` layer.
- `BusinessInfoEFPostgreContext.cs` in the `Infrastructure` project defines the database schema and entity configurations.

### Business Logic

- Business logic is implemented in the `Application` layer using the CQRS pattern with `MediatR`.
- Command and query handlers are responsible for orchestrating the execution of business rules and interacting with repositories.
- DTOs (Data Transfer Objects) are used to transfer data between the `Application` and `Presentation` layers.
