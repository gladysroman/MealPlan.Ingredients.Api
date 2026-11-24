using System.ComponentModel.DataAnnotations;

namespace MealPlan.Ingredients.Application.Common;

// [MaxLength] on a collection validates item COUNT, not the length of each item -- this
// validates that every string in the collection is at most maxLength characters.
public class MaxElementLengthAttribute(int maxLength) : ValidationAttribute
{
    public override bool IsValid(object? value) =>
        value is not IEnumerable<string> items || items.All(item => item is not null && item.Length <= maxLength);

    public override string FormatErrorMessage(string name) =>
        $"Each entry in {name} must be present and at most {maxLength} characters.";
}
