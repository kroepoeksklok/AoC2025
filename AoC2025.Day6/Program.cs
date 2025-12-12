using System.Diagnostics;

namespace AoC2025.Day6;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        var mathProblems = InputParser.ParseInput();
        var mathProblems2 = InputParser.ParseInput2();

        Stopwatch sw = new();

        sw.Start();
        SolveA(mathProblems);
        sw.Stop();
        var aDuration = sw.ElapsedTicks / 10000d;
        Console.WriteLine($"Duration {aDuration} ms");

        sw.Restart();
        SolveB(mathProblems2);
        sw.Stop();
        var bDuration = sw.ElapsedTicks / 10000d;
        Console.WriteLine($"Duration {bDuration} ms");
    }

    private static void SolveA(IEnumerable<MathProblem> mathProblems)
    {
        ulong sumOfProblems = CalcResult(mathProblems);

        Console.WriteLine($"6A answer: {sumOfProblems}");
    }

    private static void SolveB(IEnumerable<MathProblem> mathProblems)
    {
        ulong sumOfProblems = CalcResult(mathProblems);

        Console.WriteLine($"6B answer: {sumOfProblems}");
    }
    private static ulong CalcResult(IEnumerable<MathProblem> mathProblems)
    {
        ulong sumOfProblems = 0;

        foreach (var mathProblem in mathProblems)
        {
            ulong problemOutput = 0;
            if (mathProblem.Operation == Operation.Add)
            {
                problemOutput = 0;
            }
            else if (mathProblem.Operation == Operation.Multiply)
            {
                problemOutput = 1;
            }

            foreach (var input in mathProblem.Input)
            {
                if (mathProblem.Operation == Operation.Add)
                {
                    problemOutput += input;
                }
                else if (mathProblem.Operation == Operation.Multiply)
                {
                    problemOutput *= input;
                }
            }

            sumOfProblems += problemOutput;
        }

        return sumOfProblems;
    }
}
