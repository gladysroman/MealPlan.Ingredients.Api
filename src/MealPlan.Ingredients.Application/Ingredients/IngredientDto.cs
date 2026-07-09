using System.Text.Json.Serialization;

namespace MealPlan.Ingredients.Application.Ingredients;

public record IngredientDto(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("category")] string Category,
    [property: JsonPropertyName("allergens")] IReadOnlyList<string> Allergens,
    [property: JsonPropertyName("nutrition_per_100g")] NutritionDto NutritionPer100G,
    [property: JsonPropertyName("created_date")] DateOnly CreatedDate,
    [property: JsonPropertyName("updated_date")] DateOnly UpdatedDate);

public record NutritionDto(
    [property: JsonPropertyName("calories")] int Calories,
    [property: JsonPropertyName("carbs_g")] decimal CarbsG,
    [property: JsonPropertyName("protein_g")] decimal ProteinG,
    [property: JsonPropertyName("fat_g")] decimal FatG);

public record CreateIngredientRequest(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("category")] string Category,
    [property: JsonPropertyName("allergens")] List<string> Allergens,
    [property: JsonPropertyName("nutrition_per_100g")] NutritionDto NutritionPer100G);
