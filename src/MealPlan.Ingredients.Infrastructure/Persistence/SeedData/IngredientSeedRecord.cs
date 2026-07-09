using System.Text.Json.Serialization;

namespace MealPlan.Ingredients.Infrastructure.Persistence.SeedData;

internal sealed class IngredientSeedRecord
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; init; } = string.Empty;

    [JsonPropertyName("allergens")]
    public List<string> Allergens { get; init; } = [];

    [JsonPropertyName("nutrition_per_100g")]
    public NutritionSeedRecord NutritionPer100G { get; init; } = new();

    [JsonPropertyName("created_date")]
    public DateOnly CreatedDate { get; init; }

    [JsonPropertyName("updated_date")]
    public DateOnly UpdatedDate { get; init; }
}

internal sealed class NutritionSeedRecord
{
    [JsonPropertyName("calories")]
    public int Calories { get; init; }

    [JsonPropertyName("carbs_g")]
    public decimal CarbsG { get; init; }

    [JsonPropertyName("protein_g")]
    public decimal ProteinG { get; init; }

    [JsonPropertyName("fat_g")]
    public decimal FatG { get; init; }
}
