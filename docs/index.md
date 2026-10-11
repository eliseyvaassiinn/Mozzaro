# Mozzaro — Documentation

## About the project

Mozzaro is an online pizzeria application built with ASP.NET Core and Blazor.

The system allows users to browse pizzas, manage their shopping cart, register, log in, and place orders.

## Architecture

The project uses Clean Architecture and consists of the following projects:

- **Mozzaro.API** — REST API and HTTP endpoints.
- **Mozzaro.Application** — application services and business logic.
- **Mozzaro.Client** — Blazor user interface.
- **Mozzaro.Domain** — domain entities and core models.
- **Mozzaro.Infrastructure** — database access and repositories.
- **Mozzaro.Tests** — automated tests.

## Technologies

- C# and .NET 9
- ASP.NET Core Web API
- Blazor
- PostgreSQL
- Entity Framework Core
- Swagger / OpenAPI
- xUnit and Moq

## API Reference

The API reference contains documentation for controllers, services, repositories, and domain entities.

Open the **API** section in the navigation menu to explore the generated reference.

## Deployment

- Frontend: https://mozzaro-1.onrender.com
- Backend API: https://mozzaro.onrender.com
- Source code: https://github.com/eliseyvaassiinn/Mozzaro