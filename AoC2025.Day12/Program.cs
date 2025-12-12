using System.Diagnostics;

namespace AoC2025.Day12;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        var input = InputParser.ParseInput();

        Stopwatch sw = new();

        sw.Start();
        SolveA(input);
        sw.Stop();
        var aDuration = sw.ElapsedTicks / 10000d;
        Console.WriteLine($"Duration {aDuration} ms");
    }

    private static void SolveA(Input input)
    {
        int regionCount = 0;

        foreach (var treeRegion in input.TreeRegions)
        {
            int surfaceRequired = 0;

            foreach(var presentsNeeded in treeRegion.Presents)
            {
                var present = input.Presents.Single(p => p.Id == presentsNeeded.Key);
                surfaceRequired += present.Area * presentsNeeded.Value;
            }

            if(surfaceRequired <= treeRegion.Area)
            {
                regionCount++;
            }
        }

        Console.WriteLine($"12A answer: {regionCount}");
    }
}
