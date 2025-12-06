using AoC2025.Day6.Properties;
using System;
using System.Runtime.ExceptionServices;
using System.Text;

namespace AoC2025.Day6;

internal static class InputParser
{
    public static IEnumerable<MathProblem> ParseInput()
    {

        var inputString = Resources.InputDay6a;
//        var inputString =
//@"123 328  51 64 
// 45 64  387 23 
//  6 98  215 314
//*   +   *   +  ";

        var lines = inputString.Split(Environment.NewLine);
        var numberOfProblems = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Count();
        var problems = new List<MathProblem>(numberOfProblems);

        for(int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            var problemParts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for(int j = 0; j < problemParts.Length; j++)
            {
                var problem = problemParts[j];

                MathProblem mathProblem;
                if (i == 0)
                {
                    mathProblem = new MathProblem();
                    problems.Add(mathProblem);
                }

                if (i == lines.Length - 1)
                {
                    mathProblem = problems.ElementAt(j);
                    if (problem == "+")
                    {
                        mathProblem.Operation = Operation.Add;
                    }
                    else if (problem == "-")
                    {
                        mathProblem.Operation = Operation.Subtract;
                    }
                    else if (problem == "*")
                    {
                        mathProblem.Operation = Operation.Multiply;
                    }
                }
                else
                {
                    mathProblem = problems.ElementAt(j);
                    var val = ulong.Parse(problem);
                    mathProblem.Input.Add(val);
                }

            }

        }

        return problems;
    }

    public static IEnumerable<MathProblem> ParseInput2()
    {

        var inputString = Resources.InputDay6a;
//        var inputString =
//@"123 328  51 64 
// 45 64  387 23 
//  6 98  215 314
//*   +   *   +  ";

        var lines = inputString.Split(Environment.NewLine);
        var problems = new List<MathProblem>();

        var charCount = lines[0].Length;

        for(int i = 0; i < charCount; i++)
        {
            var colContents = new StringBuilder();
            for(int lineNr = lines.Length - 1; lineNr > -1; lineNr--)
            {
                var line = lines[lineNr];
                var inputChar = line[i];
                if (inputChar == ' ')
                {
                    if (lineNr == 0 && colContents.Length > 0)
                    {
                        var x = new string([.. colContents.ToString().Reverse()]);
                        var problem = problems.Last();
                        problem.Input.Add(ulong.Parse(x));
                    }
                    continue;
                }

                if(lineNr == lines.Length - 1)
                {
                    var mathProblem = new MathProblem();
                    if (inputChar == '+')
                    {
                        mathProblem.Operation = Operation.Add;
                    }
                    else if (inputChar == '-')
                    {
                        mathProblem.Operation = Operation.Subtract;
                    }
                    else if (inputChar == '*')
                    {
                        mathProblem.Operation = Operation.Multiply;
                    }
                    problems.Add(mathProblem);
                } 
                else
                {
                    colContents.Append(inputChar);
                    if(lineNr == 0)
                    {
                        var x = new string([.. colContents.ToString().Reverse()]);
                        var problem = problems.Last();
                        problem.Input.Add(ulong.Parse(x));
                    }
                }
            }
        }

        return problems;
    }
}

public sealed class MathProblem
{
    public ICollection<ulong> Input { get; }
    public Operation Operation { get; set; }

    public MathProblem()
    {
        Input = new List<ulong>();
    }
}

public enum Operation
{
    Add,
    Subtract,
    Multiply
}