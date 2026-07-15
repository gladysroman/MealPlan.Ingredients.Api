using MealPlan.Ingredients.Infrastructure.Persistence;
using MealPlan.Ingredients.Infrastructure.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MealPlan.Ingredients.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<IngredientsDbContext>(options =>
            options.UseInMemoryDatabase("MealPlanIngredients"));

        return services;
    }

    public static async Task SeedInfrastructureDataAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IngredientsDbContext>();
        await IngredientSeeder.SeedAsync(dbContext, cancellationToken);
    }
}
