# enterprise-payroll-system

A modular, enterprise-grade payroll management system built with ASP.NET Core (.NET 8) and PostgreSQL, following Clean Architecture principles.

This project demonstrates real-world backend engineering practices including JWT authentication with refresh tokens, role-based authorization, global exception handling, auditing, soft deletes, and structured logging.

Designed to be scalable, secure, and maintainable — suitable for enterprise-level applications.

Key Features

Clean Architecture (Domain, Application, Infrastructure, API)

PostgreSQL with Entity Framework Core

JWT Authentication + Refresh Tokens

Role-based Authorization

Global Exception Handling Middleware

Auditing (CreatedAt, UpdatedAt)

Soft Delete support

Repository & Service pattern

Swagger API documentation

Production-ready configuration & logging


Tech Stack

ASP.NET Core (.NET 8)

PostgreSQL

Entity Framework Core

JWT / BCrypt

Swagger / OpenAPI

Clean Architecture


React (Frontend)
   ↓ HTTP (Axios / Fetch)
.NET Web API (Backend)
   ↓ EF Core
PostgreSQL

Purpose

Built as a learning + portfolio project to practice enterprise backend patterns while integrating modern authentication and database design concepts.
