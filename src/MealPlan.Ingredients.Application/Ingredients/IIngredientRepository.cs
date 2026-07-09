using MealPlan.Ingredients.Domain.Entities;

namespace MealPlan.Ingredients.Application.Ingredients;

public interface IIngredientRepository
{
    Task<List<Ingredient>> GetAllAsync(CancellationToken cancellationToken);
    Task<Ingredient?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task AddAsync(Ingredient ingredient, CancellationToken cancellationToken);
}
