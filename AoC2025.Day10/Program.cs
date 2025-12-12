using LpSolveDotNet;
using System.Collections;

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
            var shortest = BFS(0, machine.DesiredLightConfiguration, machine.Buttons.Select(b => b.Mask));
            Console.WriteLine($"Shortest path = {shortest}");
            sumShortests += shortest;
        }

        Console.WriteLine($"10A answer: {sumShortests}");
    }

    public static int BFS(int start, int target, IEnumerable<int> buttonMasks)
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
        LpSolve.Init();
        var totalButtonPresses = 0;
        const double Ignored = 0;
        foreach (var machine in machines)
        {
            var numberOfVariables = machine.Buttons.Count();
            var numberOfConstraints = machine.RequiredJoltages.Count();
            using (var lp = LpSolve.make_lp(numberOfConstraints, numberOfVariables))
            {
                lp.set_minim();

                var buttons = new double[machine.Buttons.Count + 1];
                buttons[0] = Ignored;
                for(int i = 0; i < machine.Buttons.Count; i++)
                {
                    buttons[i + 1] = 1;
                }

                lp.set_obj_fn(buttons);

                lp.set_add_rowmode(true);

                for(int i = 0; i < machine.RequiredJoltages.Count; i++)
                {
                    var variables = new List<double>
                    {
                        Ignored
                    };

                    for (int j = 0; j < machine.Buttons.Count; j++)
                    {
                        var button = machine.Buttons[j];
                        if (button.ToggledIndices.Contains(i))
                        {
                            variables.Add(1);
                        } else
                        {
                            variables.Add(0);
                        }
                    }
                    lp.add_constraint(variables.ToArray(), lpsolve_constr_types.EQ, machine.RequiredJoltages[i]);
                }

                for(int i = 0; i < numberOfVariables; i++)
                {
                    lp.set_int(i, true);
                }

                lp.set_add_rowmode(false);
                lp.set_verbose(lpsolve_verbosity.IMPORTANT);

                lpsolve_return s = lp.solve();
                if (s == lpsolve_return.OPTIMAL)
                {
                    var objective = lp.get_objective();
                    var castedObjective = Convert.ToInt32(objective);
                    Console.WriteLine("Objective value: " + lp.get_objective());
                    //totalButtonPresses += Convert.ToInt32(lp.get_objective());
                    var results = new double[numberOfVariables];
                    lp.get_variables(results);
                    for (int j = 0; j < numberOfVariables; j++)
                    {
                        var result = results[j];
                        int castedResult = Convert.ToInt32(result);
                        Console.WriteLine(lp.get_col_name(j + 1) + ": " + result);
                        totalButtonPresses += castedResult;
                    }
                }
            }
        }

        // 15711 too low
        // 17802 too low
        // 18723 ???

        Console.WriteLine($"10B answer: {totalButtonPresses}");
    }

    public static IEnumerable<int> GetTrueIndexes(BitArray arr)
    {
        if (arr != null)
            return Enumerable.Range(0, arr.Count).Where(idx => arr.Get(idx));

        return new int[0];
    }

}
