using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;

namespace MealPlan.Ingredients.Api.Auth;

// This API has no sign-in of its own. It accepts the sign-in cookie issued by the MealPlan API,
// which it can read because both APIs share the same data-protection keys. The values below
// must match Auth/AuthenticationExtensions.cs in MealPlan.MealPlan.Api.
public static class AuthenticationExtensions
{
    private const string CookieName = "mealplan.auth";

    private const string DataProtectionApplicationName = "MealPlan";

    // Hard cap on a session's age, counted from sign-in.
    private static readonly TimeSpan AbsoluteTimeout = TimeSpan.FromDays(7);

    // Where the MealPlan API records the sign-in time inside the cookie.
    private const string SignedInAtKey = "signed_in_at";

    private static string DataProtectionKeysPath(IConfiguration configuration) =>
        configuration["DataProtection:KeysPath"]
        ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "MealPlan",
            "DataProtectionKeys");

    // A cookie with no readable sign-in time counts as expired.
    private static bool IsPastAbsoluteTimeout(AuthenticationProperties properties) =>
        !properties.Items.TryGetValue(SignedInAtKey, out var value)
        || !DateTimeOffset.TryParse(value, null, System.Globalization.DateTimeStyles.RoundtripKind, out var signedInAt)
        || DateTimeOffset.UtcNow - signedInAt > AbsoluteTimeout;

    public static IServiceCollection AddSharedCookieAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDataProtection()
            .SetApplicationName(DataProtectionApplicationName)
            .PersistKeysToFileSystem(new DirectoryInfo(DataProtectionKeysPath(configuration)));

        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = CookieName;

                // Only the MealPlan API issues and renews the cookie; this API just reads it.
                options.SlidingExpiration = false;

                // This is an API: answer 401/403 instead of redirecting to a login page.
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                };

                options.Events.OnValidatePrincipal = context =>
                {
                    if (IsPastAbsoluteTimeout(context.Properties))
                    {
                        context.RejectPrincipal();
                    }

                    return Task.CompletedTask;
                };
            });

        // Reading ingredients is open to everyone; endpoints that change them opt in with
        // RequireAuthorization().
        services.AddAuthorization();

        return services;
    }
}
