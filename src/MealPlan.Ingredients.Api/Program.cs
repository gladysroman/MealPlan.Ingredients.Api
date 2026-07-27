using MealPlan.Ingredients.Api.Endpoints;
using MealPlan.Ingredients.Application.Ingredients;
using MealPlan.Ingredients.Infrastructure;
using MealPlan.Ingredients.Repository;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure();
builder.Services.AddRepositories();
builder.Services.AddScoped<IngredientsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.MapGet("/", () => Results.Redirect("/scalar/v1"));
}

await app.Services.SeedInfrastructureDataAsync();

app.MapIngredientsEndpoints();

app.Run();
