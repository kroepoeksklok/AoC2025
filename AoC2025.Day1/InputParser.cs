using AoC2025.Day1.Properties;

namespace AoC2025.Day1;

internal static class InputParser
{
    public static IEnumerable<Rotation> ParseInput()
    {
        var rotations = new List<Rotation>();

        var lines = Resources.InputDay1a.Split(Environment.NewLine);
        foreach (var line in lines)
        {
            var direction = line[0];
            var amount = int.Parse(line.Substring(1).Trim());

            rotations.Add(new Rotation(direction == 'L' ? Direction.Left : Direction.Right, amount));
        }

        return rotations;
    }
}
