using System.Diagnostics;

namespace AoC2025.Day11;

internal class Program
{
    private static Dictionary<int, long> Memo = [];

    static void Main()
    {
        Console.WriteLine("Hello, World!");
        var nodes = InputParser.ParseInput();

        Stopwatch sw = new();

        sw.Start();
        SolveA(nodes);
        sw.Stop();
        var aDuration = sw.ElapsedTicks / 10000d;
        Console.WriteLine($"Duration {aDuration} ms");

        sw.Restart();
        SolveB(nodes);
        sw.Stop();
        var bDuration = sw.ElapsedTicks / 10000d;
        Console.WriteLine($"Duration {bDuration} ms");
    }

    private static void SolveA(IEnumerable<Node> nodes)
    {
        var startNode = nodes.Single(n => n.Label == "you");
        var targetNode = nodes.Single(n => n.Label == "out");

        var totalPaths = Start_DFS(startNode, targetNode);
        Console.WriteLine($"Answer 11A: {totalPaths}");
    }

    private static void SolveB(IEnumerable<Node> nodes)
    {
        var startNode = nodes.Single(n => n.Label == "svr");
        var fftTargetNode = nodes.Single(n => n.Label == "fft");
        var dacTargetNode = nodes.Single(n => n.Label == "dac");
        var outTargetNode = nodes.Single(n => n.Label == "out");

        var totalPathsSvrToFft = Start_DFS(startNode, fftTargetNode);
        var totalPathsFftToDac = Start_DFS(fftTargetNode, dacTargetNode);
        var totalPathsDacToOut = Start_DFS(dacTargetNode, outTargetNode);
        var svrToFftToDacToOutPaths = totalPathsSvrToFft * totalPathsFftToDac * totalPathsDacToOut;

        var totalPathsSvrToDac = Start_DFS(startNode, dacTargetNode);
        var totalPathsDacToFft = Start_DFS(dacTargetNode, fftTargetNode);
        var totalPathsFftToOut = Start_DFS(fftTargetNode, outTargetNode);
        var svrToDacToFftToOutPaths = totalPathsSvrToDac * totalPathsDacToFft * totalPathsFftToOut;

        var totalPathsFromSvrToOut = 
            svrToFftToDacToOutPaths +
            svrToDacToFftToOutPaths;
        Console.WriteLine($"Answer 11B: {totalPathsFromSvrToOut}");
    }

    private static long Start_DFS(Node startNode, Node targetNode)
    {
        Memo = [];
        return DFS(startNode, targetNode);
    }

    private static long DFS(Node startNode, Node targetNode)
    {
        if (startNode == targetNode)
        {
            Memo.TryAdd(startNode.Index, 1);
            return 1;
        }

        var pathsToTargetFromHere = 0L;

        foreach (var node in startNode.AdjacentNodes)
        {
            if (Memo.ContainsKey(node.Index))
            {
                pathsToTargetFromHere += Memo[node.Index];
            }
            else
            {
                pathsToTargetFromHere += DFS(node, targetNode);
            }
        }

        Memo.Add(startNode.Index, pathsToTargetFromHere);

        return pathsToTargetFromHere;
    }
}
