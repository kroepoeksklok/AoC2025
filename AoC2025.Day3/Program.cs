using AoC2025.Day3;
using System.Data.SqlTypes;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        var banks = InputParser.ParseInput();

        SolveA(banks);
        var now = DateTime.Now;
        SolveB(banks);
        var end = DateTime.Now;
        var duration = end - now;
        Console.WriteLine(duration.TotalMilliseconds);
    }

    private static void SolveA(IEnumerable<string> banks)
    {
        var totalOutput = 0;
        foreach (var bank in banks)
        {
            var highestJoltage = 0;
            for (int bank1 = 0; bank1 < bank.Length - 1; bank1++)
            {
                for (int bank2 = bank1 + 1; bank2 < bank.Length; bank2++)
                {
                    var combo = string.Concat(bank[bank1], bank[bank2]);
                    var joltage = int.Parse(combo);
                    if (joltage > highestJoltage)
                    {
                        highestJoltage = joltage;
                    }
                }
            }

            Console.WriteLine($"Highest joltage: {highestJoltage}");
            totalOutput += highestJoltage;
        }

        Console.WriteLine($"3A Answer: {totalOutput}");
    }

    private static void SolveB(IEnumerable<string> banks)
    {
        ulong totalOutput = 0;

        foreach (var bank in banks)
        {
            var stringSize = 12;
            var position = 0;
            var battery = "";
            var bankSub = bank;

            for(int i = 1; i <= stringSize; i++)
            {
                var substringLength = bankSub.Length - stringSize + i;
                var subString = bankSub.Substring(0, substringLength);
                var maxDigit = subString.Max();
                var maxDigitFirstIndex = bank.IndexOf(maxDigit, position);
                battery += maxDigit;
                position = maxDigitFirstIndex + 1;
                bankSub = bank.Substring(position);
            }

            var joltage = ulong.Parse(battery);

            totalOutput += joltage;
        }

        Console.WriteLine($"3B Answer: {totalOutput}");
    }
}