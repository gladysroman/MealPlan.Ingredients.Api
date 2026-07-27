using MealPlan.Ingredients.Application.Ingredients;
using MealPlan.Ingredients.Domain.Entities;
using MealPlan.Ingredients.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MealPlan.Ingredients.Repository;

public class IngredientRepository(IngredientsDbContext dbContext) : IIngredientRepository
{
    public Task<List<Ingredient>> GetAllAsync(CancellationToken cancellationToken) =>
        dbContext.Ingredients.AsNoTracking().ToListAsync(cancellationToken);

    public Task<Ingredient?> GetByIdAsync(string id, CancellationToken cancellationToken) =>
        dbContext.Ingredients.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

    public async Task<Ingredient> AddAsync(Ingredient ingredient, CancellationToken cancellationToken)
    {
        dbContext.Ingredients.Add(ingredient);
        await dbContext.SaveChangesAsync(cancellationToken);
        return ingredient;
    }

    public async Task<Ingredient> UpdateAsync(Ingredient ingredient, CancellationToken cancellationToken)
    {
        dbContext.Ingredients.Update(ingredient);
        await dbContext.SaveChangesAsync(cancellationToken);
        return ingredient;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var ingredient = await dbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        if (ingredient is null)
        {
            return;
        }

        dbContext.Ingredients.Remove(ingredient);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
