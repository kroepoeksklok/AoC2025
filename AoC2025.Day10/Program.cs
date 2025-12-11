using System.Collections;
using System.Linq;
using System.Text;

namespace AoC2025.Day10;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        var machines = InputParser.ParseInput();

        SolveA(machines);
        SolveB(machines);
    }

    private static void SolveA(IEnumerable<Machine> machines)
    {
        var sumShortests = 0;
        
        foreach(var machine in machines)
        {
            var shortest = BFS(0, machine.DesiredLightConfiguration, machine.Buttons);
            Console.WriteLine($"Shortest path = {shortest}");
            sumShortests += shortest;
        }

        Console.WriteLine($"10A answer: {sumShortests}");
    }

    public static int BFS(int start, int target, IList<int> buttonMasks)
    {
        var queue = new Queue<int>();
        var visited = new HashSet<int>();
        var distance = new Dictionary<int, int>();

        queue.Enqueue(start);
        visited.Add(start);
        distance[start] = 0;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            int dist = distance[current];

            if (current == target)
                return dist;

            foreach (var mask in buttonMasks)
            {
                int next = current ^ mask; // applying the toggle

                if (!visited.Contains(next))
                {
                    visited.Add(next);
                    distance[next] = dist + 1;
                    queue.Enqueue(next);
                }
            }
        }

        return -1; // unreachable
    }

    private static void SolveB(IEnumerable<Machine> machines)
    {
        var totalButtonPresses = 0;
        foreach (var machine in machines)
        {
            var currentJoltages = new int[machine.NumberOfLights];
            var orderedButtons = machine.Buttons.OrderByDescending(b => b);
            int buttonPresses = 0;
            foreach (var button in orderedButtons)
            {
                bool finishedWithButton = false;
                do
                {
                    BitArray b = new([button]);
                    var trues = GetTrueIndexes(b);
                    foreach (var t in trues)
                    {
                        int joltageIndex = machine.NumberOfLights - t - 1;
                        var currentJoltage = currentJoltages[joltageIndex];
                        if (currentJoltage == machine.RequiredJoltages[joltageIndex])
                        {
                            finishedWithButton = true;
                            break;
                        }
                    }

                    if (!finishedWithButton)
                    {
                        foreach (var t in trues)
                        {
                            int joltageIndex = machine.NumberOfLights - t - 1;
                            currentJoltages[joltageIndex]++;
                        }
                        buttonPresses++;
                    }
                } while (!finishedWithButton);
            }

            Console.WriteLine($"Button presses: {buttonPresses}");
            totalButtonPresses += buttonPresses;
        }

        // 15711 too low
        Console.WriteLine($"10B answer: {totalButtonPresses}");
    }

    public static IEnumerable<int> GetTrueIndexes(BitArray arr)
    {
        if (arr != null)
            return Enumerable.Range(0, arr.Count).Where(idx => arr.Get(idx));

        return new int[0];
    }

}
