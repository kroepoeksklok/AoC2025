using System.Diagnostics;

namespace AoC2025.Day4;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        var paperRollLocations = InputParser.ParseInput();

        Stopwatch sw = new();

        sw.Start();
        SolveA(paperRollLocations);
        sw.Stop();
        var aDuration = sw.ElapsedTicks / 10000d;
        Console.WriteLine($"Duration {aDuration} ms");

        sw.Restart();
        SolveB(paperRollLocations);
        sw.Stop();
        var bDuration = sw.ElapsedTicks / 10000d;
        Console.WriteLine($"Duration {bDuration} ms");
    }

    private static void SolveA(char[,] paperRollLocations)
    {
        var accessiblePaperRolls = GetNumberOfRemovableRolls(paperRollLocations);
        Console.WriteLine($"4A answer: {accessiblePaperRolls}");
    }

    private static void SolveB(char[,] paperRollLocations)
    {
        var accessiblePaperRolls = RemovePaperRolls(paperRollLocations);
        Console.WriteLine($"4B Answer: {accessiblePaperRolls}");
    }

    private static int RemovePaperRolls(char[,] paperRollLocations)
    {
        List<Tuple<int, int>> rollsToRemove = [];

        var accessiblePaperRolls = GetNumberOfRemovableRolls(paperRollLocations, (row, col) => rollsToRemove.Add(Tuple.Create(row, col)));

        if(accessiblePaperRolls == 0)
        {
            return accessiblePaperRolls;
        } 
        else
        {
            foreach (var roll in rollsToRemove)
            {
                paperRollLocations[roll.Item1, roll.Item2] = '.';
            }

            accessiblePaperRolls += RemovePaperRolls(paperRollLocations);
            return accessiblePaperRolls;
        }
    }

    private static int GetNumberOfRemovableRolls(char[,] paperRollLocations, Action<int, int>? positionHandler = null)
    {
        int accessiblePaperRolls = 0;
        int rowCount = paperRollLocations.GetLength(0);
        int columnCount = paperRollLocations.GetLength(1);

        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < columnCount; col++)
            {
                var symbol = paperRollLocations[row, col];
                if (symbol == '@')
                {
                    int adjacentPaperRolls = 0;

                    bool isTopRow = row == 0;
                    bool isLeftmostColumn = col == 0;
                    bool isRightmostColumn = col == columnCount - 1;
                    bool isBottomRow = row == rowCount - 1;

                    if (!isTopRow)
                    {
                        if (!isLeftmostColumn)
                        {
                            CheckForPaperRoll(paperRollLocations, row - 1, col - 1, ref adjacentPaperRolls);
                        }

                        CheckForPaperRoll(paperRollLocations, row - 1, col, ref adjacentPaperRolls);

                        if (!isRightmostColumn)
                        {
                            CheckForPaperRoll(paperRollLocations, row - 1, col + 1, ref adjacentPaperRolls);
                        }
                    }

                    if (!isLeftmostColumn)
                    {
                        CheckForPaperRoll(paperRollLocations, row, col - 1, ref adjacentPaperRolls);
                    }

                    if (!isRightmostColumn)
                    {
                        CheckForPaperRoll(paperRollLocations, row, col + 1, ref adjacentPaperRolls);
                    }

                    if (!isBottomRow)
                    {
                        if (!isLeftmostColumn)
                        {
                            CheckForPaperRoll(paperRollLocations, row + 1, col - 1, ref adjacentPaperRolls);
                        }

                        CheckForPaperRoll(paperRollLocations, row + 1, col, ref adjacentPaperRolls);

                        if (!isRightmostColumn)
                        {
                            CheckForPaperRoll(paperRollLocations, row + 1, col + 1, ref adjacentPaperRolls);
                        }
                    }

                    if (adjacentPaperRolls < 4)
                    {
                        accessiblePaperRolls++;
                        positionHandler?.Invoke(row, col);
                    }
                }
            }
        }

        return accessiblePaperRolls;
    }

    private static void CheckForPaperRoll(char[,] paperRollLocations, int row, int col, ref int adjacentPaperRolls)
    {
        if (paperRollLocations[row, col] == '@')
        {
            adjacentPaperRolls++;
        }
    }
}
