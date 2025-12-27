# LibraStore

LibraStore is a multi-project ASP.NET Core (.NET 8) solution that models a simple bookstore catalog using Entity Framework Core. It includes both an MVC app and a Razor Pages app, a shared data access layer with repositories/unit of work, and shared domain models.

## Solution layout

- `Libra.sln` – Visual Studio solution.
- `Libra.Models` – Domain models (`Category`, `Product`) with data annotations.
- `Libra.DataAccesss` – EF Core `ApplicationDbContext`, migrations, and repository/unit-of-work implementations.
- `Libra.Utility` – Shared utilities (placeholder for future constants/helpers).
- `LibraWeb` – MVC web app (controllers/views) wired to EF Core + repositories.
- `LibraWebRazor` – Razor Pages web app wired to EF Core + repositories.

## What’s included

- **Catalog models**: `Category` and `Product` with validation rules and display metadata.
- **EF Core context**: `ApplicationDbContext` with `Categories` and `Products` DbSets.
- **Seed data**: Pre-populated categories and products added via `OnModelCreating`.
- **Repository pattern**: Generic repository plus `CategoryRepository` and `ProductRepository`.
- **Unit of Work**: Central access to repositories and `Save()` to persist changes.
- **Two UI options**:
  - MVC app (`LibraWeb`)
  - Razor Pages app (`LibraWebRazor`)

## Prerequisites

- .NET SDK 8.0
- SQL Server (LocalDB or full SQL Server instance)

## Getting started

1. **Clone and build**

   ```bash
   dotnet build Libra.sln
   ```

2. **Configure the database**

   Each web project has its own connection string:

   - `LibraWeb/appsettings.json` → `Database=Libra`
   - `LibraWebRazor/appsettings.json` → `Database=Libra_Razor`

   Update the `Server=...` value to match your SQL Server instance.

3. **Apply migrations**

   For the MVC app:

   ```bash
   dotnet ef database update \
     --project Libra.DataAccesss \
     --startup-project LibraWeb
   ```

   For the Razor Pages app:

   ```bash
   dotnet ef database update \
     --project Libra.DataAccesss \
     --startup-project LibraWebRazor
   ```

4. **Run the app**

   MVC app:

   ```bash
   dotnet run --project LibraWeb
   ```

   Razor Pages app:

   ```bash
   dotnet run --project LibraWebRazor
   ```

## Key code references

- **DbContext & seed data**: `Libra.DataAccesss/Data/ApplicationDbContext.cs`
- **Repositories**: `Libra.DataAccesss/Repository/`
- **Domain models**: `Libra.Models/`
- **Web entry points**:
  - MVC: `LibraWeb/Program.cs`
  - Razor Pages: `LibraWebRazor/Program.cs`

## Notes

- This repo currently includes minimal UI scaffolding; it is structured for building out catalog pages, admin CRUD screens, and shopping flows.
- The `Libra.Utility` project is a placeholder for shared constants and helpers as the app grows.
