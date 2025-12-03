using AoC2025.Day2.Properties;

namespace AoC2025.Day2;

internal static class InputParser
{
    public static IEnumerable<InputRange> ParseInput()
    {
        var ranges = new List<InputRange>();

        var inputString = System.Text.Encoding.UTF8.GetString(Resources.InputDay2a);
        //var inputString = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";

        var pairs = inputString.Split(',');
        foreach(var pair in pairs)
        {
            var boundary = pair.Split('-');
            var range = new InputRange(ulong.Parse(boundary[0]), ulong.Parse(boundary[1]));
            ranges.Add(range);
        }

        return ranges;
    }
}
