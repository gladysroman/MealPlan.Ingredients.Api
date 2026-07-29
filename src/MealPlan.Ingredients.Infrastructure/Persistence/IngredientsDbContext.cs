using MealPlan.Ingredients.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MealPlan.Ingredients.Infrastructure.Persistence;

public class IngredientsDbContext(DbContextOptions<IngredientsDbContext> options) : DbContext(options)
{
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingredient>(builder =>
        {
            builder.HasKey(i => i.IngredientId);
            builder.OwnsOne(i => i.NutritionPer100G);
            builder.PrimitiveCollection(i => i.Allergens);
        });
    }
}
