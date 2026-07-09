using MealPlan.Ingredients.Application.Ingredients;
using MealPlan.Ingredients.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MealPlan.Ingredients.Infrastructure.Persistence;

public class IngredientRepository(IngredientsDbContext dbContext) : IIngredientRepository
{
    public Task<List<Ingredient>> GetAllAsync(CancellationToken cancellationToken) =>
        dbContext.Ingredients.AsNoTracking().ToListAsync(cancellationToken);

    public Task<Ingredient?> GetByIdAsync(string id, CancellationToken cancellationToken) =>
        dbContext.Ingredients.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

    public async Task AddAsync(Ingredient ingredient, CancellationToken cancellationToken)
    {
        dbContext.Ingredients.Add(ingredient);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
