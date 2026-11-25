using MealPlan.Ingredients.Api.Auth;
using MealPlan.Ingredients.Api.Endpoints;
using MealPlan.Ingredients.Application.Ingredients;
using MealPlan.Ingredients.Infrastructure;
using MealPlan.Ingredients.Repository;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedCookieAuthentication(builder.Configuration);
builder.Services.AddValidation();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapIngredientsEndpoints();

app.Run();
