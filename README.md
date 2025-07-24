# EShopMicroservices

A cloud-native e-commerce microservices solution built with .NET 8, following best practices in Clean Architecture, DDD, and modern DevOps. This project demonstrates a modular, scalable, and production-ready architecture using the latest .NET technologies and a modern React-based frontend.

## Features

- **Microservices**: Product, Basket, Discount, and Ordering services, each with dedicated databases and responsibilities.
- **API Gateway**: YARP Reverse Proxy for unified API access, routing, and rate limiting.
- **Event-Driven Communication**: RabbitMQ and MassTransit for async messaging and integration events.
- **CQRS & DDD**: MediatR, FluentValidation, and Mapster for clean separation of concerns and maintainable code.
- **Databases**: PostgreSQL (Marten DocumentDB), Redis, SQLite, and SQL Server support.
- **Containerization**: Docker and Docker Compose for local development and deployment.
- **Observability**: Logging, global exception handling, and health checks.
- **Frontend**: Modern React app using shadcn and Tailwind CSS for a responsive, accessible UI.

## Microservices Overview

- **Catalog**: Minimal APIs, Vertical Slice Architecture, CQRS, Marten (PostgreSQL), Carter, and health checks.
- **Basket**: RESTful API, Redis cache, design patterns (Proxy, Decorator), gRPC integration, RabbitMQ event publishing.
- **Discount**: gRPC server, EF Core with SQLite, Protobuf contracts, containerized database.
- **Ordering**: DDD, CQRS, Clean Architecture, EF Core (SQL Server), domain/integration events, RabbitMQ event consumption.
- **API Gateway**: YARP for routing, clustering, and rate limiting.

## Frontend

- **React**: Modern SPA consuming API Gateway endpoints.
- **shadcn**: Accessible, customizable UI components.
- **Tailwind CSS**: Utility-first styling for rapid UI development.

## Getting Started

1. Clone the repository.
2. Run `docker-compose up` to start all services and dependencies.
3. Navigate to the `webui` folder and run the React app (`npm install && npm start`).
4. Access the application via the provided URL.

## Architecture Highlights

- Clean, layered architecture (Core, Application, Infrastructure, Presentation).
- Domain-Driven Design (Entities, Repositories, Services, DTOs).
- SOLID principles and design patterns (Dependency Injection, logging, validation, exception handling).
- Loosely-coupled, scalable, and maintainable codebase.

## License

This project is open-source and available under the MIT License.