# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project state

This is an **early-stage scaffold** of a gym management backend (.NET 9 Web API), not a running application yet. Much of the wiring is stubbed:

- `Program.cs` only registers controllers + OpenAPI. It does **not** yet call `AddInfrastructure`, MediatR, or reference the Application/Infra/Domain projects (the API `.csproj` has no project references). The only controller is the default `WeatherForecastController`.
- `Infra/DependecyInjection.cs` registers EF Core + interceptors but leaves repositories and services commented out.
- Several types are placeholders: `MemberId` is an empty class, `Application/Members/IMemberRepository.cs` is an empty `internal class` (not an interface), and `Domain/Members/Specifications` / `Application/Members/{Commands,Queries}` are empty folders.
- `GymManagementDbContext` `DbSet` names are leftover from a template (`Tenants` maps to `Member`, `Clients` maps to `Membership`).

When extending the system, expect to *complete* these stubs rather than work around finished code. Much of the structure (commented-out services, audit/tenant abstractions) is carried over from a prior multi-tenant SaaS template.

## Commands

```bash
# Build the whole solution
dotnet build GymManagementSystem.sln

# Run the API (from repo root). Profiles: http (:5168) or https (:7281)
dotnet run --project src/GymManagementSystem.API
dotnet run --project src/GymManagementSystem.API --launch-profile https

# EF Core migrations (Infra holds the DbContext, API is the startup project once wired)
dotnet ef migrations add <Name> --project src/GymManagementSystem.Infra --startup-project src/GymManagementSystem.API
dotnet ef database update --project src/GymManagementSystem.Infra --startup-project src/GymManagementSystem.API
```

There is **no test project** in the solution yet.

The database is **PostgreSQL** (Npgsql). The connection string is read from configuration key `ConnectionStrings:DbConnection` — currently absent from `appsettings.json`, so add it (e.g. in `appsettings.Development.json` or user-secrets) before running anything that touches the DB.

## Tech Stack
Runtime - .NET 9
Database - PosgtgresSQL - Entity Framework - Npgsql
Ardalis Specification
Validation - FluentValidation
Architecture - Clean Architecture - Vertical Slice Architecture - CQRS
Tools - Scalar OpenAPI UI - Health Checks - Logging Decorators - EF Core
Audit Interceptor

## Architecture Layers
WebApi -> Infrastructure -> Application -> Domain
Rules:
Domain - Contains entities, enums, business rules, specifications, domain events, contracts rules, domain exceptions and abstractions - Must not depend on any other layer.
Application - Contains features (vertical slices), Each feature folder will contain: static class errors, commands, queries, interface repository, extension mappers, class response mapper
and a Shared folder with shared contracts, shared interfaces, shared classes response- Depends only on Domain
Infrastructure - Implements persistence, repositories, interceptors, configurations, internal services and external services - Depends on Application and Domain
WebApi - Application entry point - Composes dependencies and middleware
Shared — cross-cutting primitives with no domain knowledge.

### Key patterns

- **Domain events**: Aggregates collect events via `AddDomainEvent`. `DispatchDomainEventsInterceptor` (an EF `SaveChangesInterceptor`) publishes them through MediatR *during* `SaveChanges`, then clears them. This means events fire inside the same transaction as the save.
- **Auditing**: `AuditableEntityInterceptor` stamps `CreatedAt/CreatedBy/LastModified/...` on every `IEntity` on save, pulling identity from `IUserContext`. The base `Entity<T>` carries all audit columns.
- **Business rules**: `IBusinessRule` implementations (e.g. `EmailMustNotBeUsed`) are validated via `Aggregate.CheckRule(...)`, which throws `DomainException` when `IsBroken()`. Rules that need data access take an injected `IMemberRuleCheck`-style contract.
- **CQRS markers** (in `Shared/Common/CRQS`): `ICommand`/`ICommandHandler`/`IQuery`/`IQueryHandler` wrap MediatR's `IRequest`/`IRequestHandler`. Use these instead of raw MediatR interfaces.
- **Result pattern** (`Shared/Common/ResultPattern`): return `Result` / `Result<T>` with `Error`/`ErrorType` rather than throwing for expected failures. Has implicit conversion from `Error` and `[JsonIgnore]` on a default `Error`.
- **DateTime → `timestamp`**: `OnModelCreating` globally maps all `DateTime`/`DateTime?` properties (including on owned types) to Postgres `timestamp`.

### Conventions

- Aggregates/entities use private parameterless constructors (for EF) + a public/internal constructor for real creation; setters are `private`/`internal` so state changes go through methods (e.g. `Member.CreateMembership`, `Membership.Cancel`).
- New EF mappings go in `Infra/Data/Configurations/` (auto-discovered via `ApplyConfigurationsFromAssembly`).
- Note the namespace/folder `GymManagementSystem.Infra` is spelled `Infra` and the DI file is `DependecyInjection.cs` (existing typo — match it when referencing).
