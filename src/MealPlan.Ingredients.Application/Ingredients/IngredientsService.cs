using MealPlan.Ingredients.Domain.Entities;

namespace MealPlan.Ingredients.Application.Ingredients;

public class IngredientsService(IIngredientRepository repository)
{
    public Task<List<Ingredient>> GetIngredientsAsync(CancellationToken cancellationToken) =>
        repository.GetAllAsync(cancellationToken);

    public Task<Ingredient?> GetIngredientByIdAsync(string id, CancellationToken cancellationToken) =>
        repository.GetByIdAsync(id, cancellationToken);

    public Task AddIngredientAsync(Ingredient ingredient, CancellationToken cancellationToken) =>
        repository.AddAsync(ingredient, cancellationToken);
}
