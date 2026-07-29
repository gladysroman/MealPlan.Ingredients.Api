using MealPlan.Ingredients.Domain.Entities;

namespace MealPlan.Ingredients.Application.Ingredients;

public interface IIngredientRepository
{
    Task<List<Ingredient>> GetAllAsync(CancellationToken cancellationToken);
    Task<Ingredient?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<Ingredient> AddAsync(Ingredient ingredient, CancellationToken cancellationToken);
    Task<Ingredient> UpdateAsync(Ingredient ingredient, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(string id, CancellationToken cancellationToken);
}
