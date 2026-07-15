using MealPlan.Ingredients.Application.Ingredients;
using MealPlan.Ingredients.Domain.Entities;
using MealPlan.Ingredients.Infrastructure;
using MealPlan.Ingredients.Repository;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure();
builder.Services.AddRepositories();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

await app.Services.SeedInfrastructureDataAsync();

var ingredients = app.MapGroup("/ingredients");

ingredients.MapGet("/", async (IIngredientRepository repository, CancellationToken cancellationToken) =>
{
    var results = await repository.GetAllAsync(cancellationToken);
    return results.Select(ToDto);
});

ingredients.MapGet("/{id}", async (string id, IIngredientRepository repository, CancellationToken cancellationToken) =>
{
    var ingredient = await repository.GetByIdAsync(id, cancellationToken);
    return ingredient is null ? Results.NotFound() : Results.Ok(ToDto(ingredient));
});

ingredients.MapPost("/", async (CreateIngredientRequest request, IIngredientRepository repository, CancellationToken cancellationToken) =>
{
    var today = DateOnly.FromDateTime(DateTime.UtcNow);
    var ingredient = new Ingredient
    {
        Id = $"ing_{Guid.NewGuid():N}",
        Name = request.Name,
        Category = request.Category,
        Allergens = request.Allergens,
        NutritionPer100G = new NutritionInfo
        {
            Calories = request.NutritionPer100G.Calories,
            CarbsG = request.NutritionPer100G.CarbsG,
            ProteinG = request.NutritionPer100G.ProteinG,
            FatG = request.NutritionPer100G.FatG
        },
        CreatedDate = today,
        UpdatedDate = today
    };

    await repository.AddAsync(ingredient, cancellationToken);

    return Results.Created($"/ingredients/{ingredient.Id}", ToDto(ingredient));
});

app.Run();

static IngredientDto ToDto(Ingredient ingredient) => new(
    ingredient.Id,
    ingredient.Name,
    ingredient.Category,
    ingredient.Allergens,
    new NutritionDto(
        ingredient.NutritionPer100G.Calories,
        ingredient.NutritionPer100G.CarbsG,
        ingredient.NutritionPer100G.ProteinG,
        ingredient.NutritionPer100G.FatG),
    ingredient.CreatedDate,
    ingredient.UpdatedDate);
