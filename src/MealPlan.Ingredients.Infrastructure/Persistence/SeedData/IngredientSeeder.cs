using System.Text.Json;
using MealPlan.Ingredients.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MealPlan.Ingredients.Infrastructure.Persistence.SeedData;

internal static class IngredientSeeder
{
    private static readonly string SeedFilePath =
        Path.Combine(AppContext.BaseDirectory, "Persistence", "SeedData", "ingredients.json");

    public static async Task SeedAsync(IngredientsDbContext dbContext, CancellationToken cancellationToken)
    {
        if (await dbContext.Ingredients.AnyAsync(cancellationToken))
        {
            return;
        }

        await using var stream = File.OpenRead(SeedFilePath);
        var records = await JsonSerializer.DeserializeAsync<List<IngredientSeedRecord>>(stream, cancellationToken: cancellationToken)
            ?? [];

        var ingredients = records.Select(record => new Ingredient
        {
            IngredientId = record.IngredientId,
            Name = record.Name,
            Category = record.Category,
            Allergens = record.Allergens,
            NutritionPer100G = new NutritionInfo
            {
                Calories = record.NutritionPer100G.Calories,
                CarbsG = record.NutritionPer100G.CarbsG,
                ProteinG = record.NutritionPer100G.ProteinG,
                FatG = record.NutritionPer100G.FatG
            },
            CreatedDate = record.CreatedDate,
            UpdatedDate = record.UpdatedDate
        });

        dbContext.Ingredients.AddRange(ingredients);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
