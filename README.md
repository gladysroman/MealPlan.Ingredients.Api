# MealPlan.Ingredients.Api

Minimal API built on .NET 10 using Clean Architecture.

## Projects

- `MealPlan.Ingredients.Domain` — entities, no dependencies
- `MealPlan.Ingredients.Application` — interfaces and DTOs, depends on Domain
- `MealPlan.Ingredients.Infrastructure` — EF Core (in-memory) implementation, depends on Application
- `MealPlan.Ingredients.Api` — minimal API host, depends on Application and Infrastructure

## Run

```
dotnet run --project src/MealPlan.Ingredients.Api
```

Sample endpoints (under `/ingredients`): `GET /`, `GET /{id}`, `POST /`.
# MealPlan.Ingredients
