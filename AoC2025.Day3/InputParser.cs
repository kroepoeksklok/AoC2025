using AoC2025.Day3.Properties;

namespace AoC2025.Day3;

internal static class InputParser
{
    public static IEnumerable<string> ParseInput()
    {
        var ranges = new List<string>();

        var inputString = System.Text.Encoding.UTF8.GetString(Resources.InputDay3a);
//        var inputString =
//@"987654321111111
//811111111111119
//234234234234278
//818181911112111";

        var lines = inputString.Split(Environment.NewLine);

        return lines;
    }
}
