namespace AoC2025.Day5;

internal sealed record IngredientDatabase(IEnumerable<InputRange> FreshIngredients, IEnumerable<ulong> AvailableIngredients);