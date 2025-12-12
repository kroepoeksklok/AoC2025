using System.Diagnostics;

namespace AoC2025.Day5;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        var ingredientDatabase = InputParser.ParseInput();

        Stopwatch sw = new();

        sw.Start();
        SolveA(ingredientDatabase);
        sw.Stop();
        var aDuration = sw.ElapsedTicks / 10000d;
        Console.WriteLine($"Duration {aDuration} ms");

        sw.Restart();
        SolveB(ingredientDatabase);
        sw.Stop();
        var bDuration = sw.ElapsedTicks / 10000d;
        Console.WriteLine($"Duration {bDuration} ms");
    }

    private static void SolveA(IngredientDatabase ingredientDatabase)
    {
        int freshIngredientCount = 0;

        foreach (var ingredient in ingredientDatabase.AvailableIngredients)
        {
            if (ingredientDatabase.FreshIngredients.Any(x => x.Start <= ingredient && x.End >= ingredient))
            {
                freshIngredientCount++;
            }
        }

        Console.WriteLine($"5A answer: {freshIngredientCount}");
    }

    private static void SolveB(IngredientDatabase ingredientDatabase)
    {
        var orderedFreshIds = ingredientDatabase.FreshIngredients.OrderBy(x => x.Start).ToList();

        List<InputRange> greedyIds = [];
        for (int i = 0; i < orderedFreshIds.Count; i++)
        {
            var currentRange = orderedFreshIds[i];
            var overlappingExisting = greedyIds.FirstOrDefault(x => currentRange.Start <= x.End);
            if (overlappingExisting != null)
            {
                // The new range starts in an existing greedy range
                if (currentRange.End > overlappingExisting.End)
                {
                    overlappingExisting.End = currentRange.End;
                }
            }
            else
            {
                // No overlap, so add new range
                greedyIds.Add(new InputRange(currentRange.Start, currentRange.End));
            }
        }

        ulong ingredientCountInRange = 0;
        foreach (var range in greedyIds)
        {
            ingredientCountInRange += range.End - range.Start + 1;
        }

        Console.WriteLine($"5B Answer: {ingredientCountInRange}");
    }
}
