Hoan Trong ## E-Commerce Project

** Built with ASP.NET Core 8 and Two Databases**

SQL Server: Optimized for transactional data
MongoDB: Scalable for high-performance reads
** Leveraging CQRS for Performance and Maintainability**

Command Query Responsibility Segregation (CQRS): Separates read and write operations for better performance and scalability.

✨ Technologies ✨ 

ASP.NET Core 8
AutoMapper (Automatic mapping)
FluentValidator (Data validation)
MediatR (Request/response pipeline)
Swagger UI  (API documentation)
SQL Server (Write side)
MongoDB (Read side)

⚡ Architecture ⚡
src
│   ├───E.API
│   │   │   ├───Agregrates
│   │   │   ├───Contracts
│   │   │   │   ├───Brands
│   │   │   │   │   ├───Requests
│   │   │   │   │   └───Responses
│   │   │   │   ├───Categories
│   │   │   │   │   ├───Requests
│   │   │   │   │   └───Responses
│   │   │   │   ├───Common
│   │   │   │   ├───Identities
│   │   │   │   ├───Products
│   │   │   │   │   ├───Requests
│   │   │   │   │   └───Responses
│   │   │   │   └───Users
│   │   │   │       ├───Requests
│   │   │   │       └───Responses
│   │   │   ├───Controllers
│   │   │   │   ├───V1
│   │   │   │   └───V2
│   │   │   ├───Extension
│   │   │   ├───MappingProfiles
│   │   │   ├───Properties
│   │   │   └───Registrars
│   │   │       ├───Database
│   │   │       ├───DependencyInjection
│   │   │       ├───Extensions
│   │   │       ├───Identity
│   │   │       ├───MVC
│   │   │       ├───RegistrarBase
│   │   │       └───Swagger
│   │   │           └───Options
│   ├───E.Application
│   │   ├───Brands
│   │   │   ├───CommandHandlers
│   │   │   ├───Commands
│   │   │   ├───EventHandlers
│   │   │   ├───Queries
│   │   │   └───QueryHandlers
│   │   ├───Categories
│   │   │   ├───CommandHanlders
│   │   │   └───Commands
│   │   ├───Enums
│   │   ├───Identity
│   │   │   ├───CommandHandlers
│   │   │   ├───Commands
│   │   │   ├───EventHandlers
│   │   │   ├───Options
│   │   │   ├───Queries
│   │   │   └───QueryHandlers
│   │   ├───Models
│   │   ├───Products
│   │   │   ├───CommandHandlers
│   │   │   ├───Commands
│   │   │   ├───EventHandlers
│   │   │   ├───Queries
│   │   │   └───QueryHandlers
│   │   ├───Properties
│   │   └───Services
│   ├───E.Infrastructure
│   │   ├───EventPublishers
│   │   ├───Extension
│   │   ├───Repository
│   │   └───UoW
│   └───E.Domain
│       ├───Entities
│       │   ├───Brands
│       │   │   ├───BrandValidators
│       │   │   └───Events
│       │   ├───Categories
│       │   │   ├───CategoryValidators
│       │   │   └───Events
│       │   ├───Comments
│       │   │   └───CommentValidators
│       │   ├───Products
│       │   │   ├───Events
│       │   │   └───ProductValidators
│       │   └───Users
│       │       ├───Dto
│       │       ├───Events
│       │       └───UserValidators
│       ├───Exceptions

SOLID Principles (Software design principles)
Clean Code Practices (Maintainable code)
Domain-Driven Design (DDD) (Modeling complex business logic)
Domain Events, Notifications, Validations (Enriching domain logic)
Unit of Work, Repository Pattern, Result Pattern (Data access and handling)

🌟 Give it a Star 🌟
💖 Hoan Trong thanks very much 💖
