# MealPlan.Ingredients.Api

Minimal API built on .NET 10 using Clean Architecture.

## Projects

- `MealPlan.Ingredients.Domain` — entities, no dependencies
- `MealPlan.Ingredients.Application` — repository interface and DTOs, depends on Domain
- `MealPlan.Ingredients.Infrastructure` — EF Core (in-memory) `DbContext` and JSON seed data, depends on Application
- `MealPlan.Ingredients.Repository` — `IIngredientRepository` implementation over the `DbContext`, depends on Application and Infrastructure
- `MealPlan.Ingredients.Api` — minimal API host, depends on Application, Infrastructure, and Repository

## Run

```
dotnet run --project src/MealPlan.Ingredients.Api
```

## Endpoints

All under `/ingredients`:

| Method   | Path    | Answers                                                    |
| -------- | ------- | ---------------------------------------------------------- |
| `GET`    | `/`     | 200 with every ingredient                                  |
| `GET`    | `/{id}` | 200 with the ingredient, or 204 when there is none         |
| `POST`   | `/`     | 201 with the new ingredient, 400, or 401                   |
| `PUT`    | `/{id}` | 200 with the updated ingredient, 400, 401, or 404          |
| `DELETE` | `/{id}` | 204, 401, or 404                                           |

## Sign-in

Reading is open to everyone. `POST`, `PUT` and `DELETE` need a signed-in user and answer 401
otherwise.

This API has no sign-in of its own: it accepts the `mealplan.auth` cookie issued by
`MealPlan.MealPlan.Api`. For that to work both APIs must read the same data-protection keys.
By default each uses `%LOCALAPPDATA%\MealPlan\DataProtectionKeys`; set `DataProtection:KeysPath`
to the same folder in both to use another location (required when they run as different
users or on different machines). See `Auth/AuthenticationExtensions.cs`.
