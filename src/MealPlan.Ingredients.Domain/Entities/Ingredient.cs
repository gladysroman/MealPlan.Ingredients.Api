namespace MealPlan.Ingredients.Domain.Entities;

public class Ingredient
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public List<string> Allergens { get; set; } = [];
    public NutritionInfo NutritionPer100G { get; set; } = new();
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
