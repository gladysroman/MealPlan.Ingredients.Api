using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MealPlan.Ingredients.Application.Common;

namespace MealPlan.Ingredients.Application.Ingredients;

public record IngredientDto(
    [property: JsonPropertyName("ingredient_id")] string IngredientId,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("category")] string Category,
    [property: JsonPropertyName("allergens")] IReadOnlyList<string> Allergens,
    [property: JsonPropertyName("nutrition_per_100g")] NutritionDto NutritionPer100G,
    [property: JsonPropertyName("created_date")] DateTime CreatedDate,
    [property: JsonPropertyName("updated_date")] DateTime UpdatedDate);

// Values are per 100 g, so no macronutrient can exceed 100 g and pure fat tops out near 900 kcal.
public record NutritionDto(
    [property: JsonPropertyName("calories"), Range(0, 1000)] int Calories,
    [property: JsonPropertyName("carbs_g"), Range(0, 100)] decimal CarbsG,
    [property: JsonPropertyName("protein_g"), Range(0, 100)] decimal ProteinG,
    [property: JsonPropertyName("fat_g"), Range(0, 100)] decimal FatG);

public record CreateIngredientRequest(
    [property: JsonPropertyName("name"), Required, MaxLength(100)] string Name,
    [property: JsonPropertyName("category"), Required, MaxLength(50)] string Category,
    [property: JsonPropertyName("allergens"), Required, MaxLength(20), MaxElementLength(50)] List<string> Allergens,
    [property: JsonPropertyName("nutrition_per_100g"), Required] NutritionDto NutritionPer100G);

public record UpdateIngredientRequest(
    [property: JsonPropertyName("name"), Required, MaxLength(100)] string Name,
    [property: JsonPropertyName("category"), Required, MaxLength(50)] string Category,
    [property: JsonPropertyName("allergens"), Required, MaxLength(20), MaxElementLength(50)] List<string> Allergens,
    [property: JsonPropertyName("nutrition_per_100g"), Required] NutritionDto NutritionPer100G);
