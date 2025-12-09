using AoC2025.Day9.Properties;
using System;
using System.Text;

namespace AoC2025.Day9;

internal static class InputParser
{
    public static IList<Coordinate> ParseInput()
    {

        //var inputString = System.Text.Encoding.UTF8.GetString(Resources.InputDay9a);
        var inputString =
@"7,1
11,1
11,7
9,7
9,5
2,5
2,3
7,3";
        var ranges = new List<Coordinate>();
        var lines = inputString.Split(Environment.NewLine);
        
        foreach(var line in lines)
        {
            var boundary = line.Split(',');
            var range = new Coordinate(ulong.Parse(boundary[0]), ulong.Parse(boundary[1]));
            ranges.Add(range);
        }

        return ranges;
    }
}

internal sealed record Coordinate(ulong X, ulong Y);