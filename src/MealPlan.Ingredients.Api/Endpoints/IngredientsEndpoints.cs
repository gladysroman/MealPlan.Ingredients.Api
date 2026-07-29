using MealPlan.Ingredients.Application.Ingredients;
using MealPlan.Ingredients.Domain.Entities;

namespace MealPlan.Ingredients.Api.Endpoints;

public static class IngredientsEndpoints
{
    public static void MapIngredientsEndpoints(this WebApplication app)
    {
        var ingredients = app.MapGroup("/ingredients");

        ingredients.MapGet("/", async (IngredientsService service, CancellationToken cancellationToken) =>
        {
            var results = await service.GetIngredientsAsync(cancellationToken);
            return results.Select(ToDto);
        });

        ingredients.MapGet("/{id}", async (string id, IngredientsService service, CancellationToken cancellationToken) =>
        {
            var ingredient = await service.GetIngredientByIdAsync(id, cancellationToken);
            return ingredient is null ? Results.NoContent() : Results.Ok(ToDto(ingredient));
        });

        ingredients.MapPost("/create", async (CreateIngredientRequest request, IngredientsService service, CancellationToken cancellationToken) =>
        {
            var ingredient = await service.AddIngredientAsync(request, cancellationToken);

            return Results.Created($"/ingredients/{ingredient.IngredientId}", ToDto(ingredient));
        });

        ingredients.MapPut("/{id}", async (string id, UpdateIngredientRequest request, IngredientsService service, CancellationToken cancellationToken) =>
        {
            var ingredient = await service.UpdateIngredientAsync(id, request, cancellationToken);

            return ingredient is null
                ? Results.NotFound($"Ingredient '{id}' was not found.")
                : Results.Ok(ToDto(ingredient));
        });

        ingredients.MapDelete("/{id}", async (string id, IngredientsService service, CancellationToken cancellationToken) =>
        {
            var deleted = await service.DeleteIngredientAsync(id, cancellationToken);

            return deleted
                ? Results.NoContent()
                : Results.NotFound($"Ingredient '{id}' was not found.");
        });
    }

    private static IngredientDto ToDto(Ingredient ingredient) => new(
        ingredient.IngredientId,
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
}
