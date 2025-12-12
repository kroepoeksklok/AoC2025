using System.Diagnostics;
using System.Text;

namespace AoC2025.Day2;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        var ranges = InputParser.ParseInput();

        Stopwatch sw = new();

        sw.Start();
        SolveA(ranges);
        sw.Stop();
        var aDuration = sw.ElapsedTicks / 10000d;
        Console.WriteLine($"Duration {aDuration} ms");

        sw.Restart();
        SolveB(ranges);
        sw.Stop();
        var bDuration = sw.ElapsedTicks / 10000d;
        Console.WriteLine($"Duration {bDuration} ms");
    }

    private static void SolveA(IEnumerable<InputRange> ranges)
    {
        ulong sumInvalidIds = 0;

        foreach (var range in ranges)
        {
            for (var i = range.Start; i <= range.End; i++)
            {
                var iString = i.ToString();
                if (iString.Length % 2 == 1)
                {
                    // Filter out odd number of characters
                    continue;
                }

                var firstHalf = iString[..(iString.Length / 2)];
                var secondHalf = iString[(iString.Length / 2)..];

                if (firstHalf == secondHalf)
                {
                    sumInvalidIds += i;
                }
            }
        }

        Console.WriteLine($"2A Answer: {sumInvalidIds}");
    }

    private static void SolveB(IEnumerable<InputRange> ranges)
    {
        ulong sumInvalidIds = 0;

        foreach (var range in ranges)
        {
            for (var i = range.Start; i <= range.End; i++)
            {
                var iString = i.ToString();

                List<string> substrings = [];
                var halfway = iString.Length / 2;
                for (var m = 0; m <= halfway; m++)
                {
                    var substring = iString[..(m + 1)];
                    if (iString.Length % substring.Length == 0)
                    {
                        // Only add substrings whose length evenly divides the original string's length
                        substrings.Add(substring);
                    }
                }

                foreach (var substring in substrings)
                {
                    StringBuilder sb = new(substring);
                    do
                    {
                        sb.Append(substring);
                    } while (sb.Length < iString.Length);

                    if (sb.Length > iString.Length)
                    {
                        continue;
                    }

                    if (sb.Length == iString.Length && sb.ToString() == iString)
                    {
                        sumInvalidIds += i;
                        break;
                    }
                }
            }
        }

        Console.WriteLine($"2B Answer: {sumInvalidIds}");
    }
}
