using MealPlan.Ingredients.Domain.Entities;

namespace MealPlan.Ingredients.Application.Ingredients;

public class IngredientsService(IIngredientRepository repository)
{
    public Task<List<Ingredient>> GetIngredientsAsync(CancellationToken cancellationToken) =>
        repository.GetAllAsync(cancellationToken);

    public Task<Ingredient?> GetIngredientByIdAsync(string id, CancellationToken cancellationToken) =>
        repository.GetByIdAsync(id, cancellationToken);

    public async Task<Ingredient> AddIngredientAsync(CreateIngredientRequest request, CancellationToken cancellationToken) {
        var date = DateTime.UtcNow;
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
        CreatedDate = date,
        UpdatedDate = date
    };
        return await repository.AddAsync(ingredient, cancellationToken);
    }

    public async Task<Ingredient?> UpdateIngredientAsync(string id, UpdateIngredientRequest request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return null;
        }

        existing.Name = request.Name;
        existing.Category = request.Category;
        existing.Allergens = request.Allergens;
        existing.NutritionPer100G = new NutritionInfo
        {
            Calories = request.NutritionPer100G.Calories,
            CarbsG = request.NutritionPer100G.CarbsG,
            ProteinG = request.NutritionPer100G.ProteinG,
            FatG = request.NutritionPer100G.FatG
        };
        existing.UpdatedDate = DateTime.UtcNow;

        return await repository.UpdateAsync(existing, cancellationToken);
    }

    public Task<bool> DeleteIngredientAsync(string id, CancellationToken cancellationToken) =>
        repository.DeleteAsync(id, cancellationToken);
}
