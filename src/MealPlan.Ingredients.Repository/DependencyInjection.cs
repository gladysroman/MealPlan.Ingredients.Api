using MealPlan.Ingredients.Application.Ingredients;
using Microsoft.Extensions.DependencyInjection;

namespace MealPlan.Ingredients.Repository;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IIngredientRepository, IngredientRepository>();

        return services;
    }
}
