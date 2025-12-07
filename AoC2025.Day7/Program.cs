namespace AoC2025.Day7;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        var manifold = InputParser.ParseInput();

        SolveA(manifold);
        SolveB(manifold);
    }

    private static void SolveA(char[,] manifold)
    {
        (int Row, int Column) startCoordinates = GetStartCoordindates(manifold);

        List<Tuple<int, int>> hitSplitters = [];
        TraverseManifold(manifold, hitSplitters, startCoordinates.Row, startCoordinates.Column);
        Console.WriteLine($"7A answer: {hitSplitters.Count}");
    }

    private static void TraverseManifold(char[,] manifold, List<Tuple<int, int>> hitSplitters, int rowStart, int colStart)
    {
        int totalRows = manifold.GetLength(0);
        for(int i = rowStart + 1; i < totalRows; i++)
        {
            if (manifold[i, colStart] == '^')
            {
                var splitter = hitSplitters.FirstOrDefault(t => t.Item1 == i && t.Item2 == colStart);
                if(splitter == null)
                {
                    splitter = Tuple.Create(i, colStart);
                    hitSplitters.Add(splitter);

                    TraverseManifold(manifold, hitSplitters, i, colStart - 1);
                    TraverseManifold(manifold, hitSplitters, i, colStart + 1);
                }

                break;
            }
        }
    }

    private static void SolveB(char[,] manifold)
    {
        (int Row, int Column) startCoordinates = GetStartCoordindates(manifold);

        var exitCounts = new List<SplitterExitCount>();
        var x = GetPathCount(manifold, exitCounts, startCoordinates.Row, startCoordinates.Column);
        Console.WriteLine($"7B answer: {x}");
    }

    private static ulong GetPathCount(char[,] manifold, List<SplitterExitCount> splitterExitCounts, int rowStart, int colStart)
    {
        int totalRows = manifold.GetLength(0);

        for (int i = rowStart + 1; i < totalRows; i++)
        {
            if (manifold[i, colStart] == '.' && i == totalRows - 1)
            {
                return 1;
            }
            else if (manifold[i, colStart] == '^')
            {
                var splitter = splitterExitCounts.FirstOrDefault(t => t.Row == i && t.Column == colStart);
                if (splitter == null)
                {
                    splitter = new SplitterExitCount
                    {
                        Row = i,
                        Column = colStart,
                        ExitCount = 0
                    };
                    splitterExitCounts.Add(splitter);

                    var leftExitCount = GetPathCount(manifold, splitterExitCounts, i, colStart - 1);
                    var rightExitCount = GetPathCount(manifold, splitterExitCounts, i, colStart + 1);

                    splitter.ExitCount += leftExitCount;
                    splitter.ExitCount += rightExitCount;
                } 

                return splitter.ExitCount;
            }
        }

        return 0;
    }

    private static (int Row, int Column) GetStartCoordindates(char[,] manifold)
    {
        int startColumn = 0;
        for (int i = 0; i < manifold.GetLength(1); i++)
        {
            var colContent = manifold[0, i];
            if (colContent == 'S')
            {
                startColumn = i;
            }
        }

        return (0, startColumn);
    }

    private class SplitterExitCount
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public ulong ExitCount { get; set; }
    }
}
