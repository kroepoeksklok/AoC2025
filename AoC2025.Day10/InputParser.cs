using AoC2025.Day10.Properties;
using System.Text.RegularExpressions;

namespace AoC2025.Day10;

internal static partial class InputParser
{
    public static IEnumerable<Machine> ParseInput()
    {
        var inputString = System.Text.Encoding.UTF8.GetString(Resources.InputDay10a);
        //var inputString =
        //@"[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
        //[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
        //[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}";
        //var inputString = "[#.#####] (2,3,4,6) (2,5) (1,3,4,5,6) (1,2,5,6) (0,5,6) (0,1,2,3,4,6) (1,2,3,5,6) (1,3,4,6) (0,2,3,4,5,6) {23,42,62,53,35,62,74}";

        var machines = new List<Machine>();

        var lines = inputString.Split(Environment.NewLine);

        for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
        {
            var line = lines[lineNumber];
            List<Button> buttons = [];
            var matches = MachineRegex().Matches(line);
            int desiredLightMask = 0;
            int numberOfLights = 0;
            List<int> joltages = [];

            for (var i = 0; i < matches.Count; i++)
            {
                if (i == 0)
                {
                    var desiredLightConfiguration = matches[i].Value;

                    numberOfLights = desiredLightConfiguration.Length;

                    desiredLightMask = desiredLightConfiguration
                        .Select((c, i) => c == '#' ? 1 << (numberOfLights - 1 - i) : 0)
                         .Aggregate(0, (a, b) => a | b);
                }
                else if (i == matches.Count - 1)
                {
                    joltages = matches[i].Value.Split(',').Select(int.Parse).ToList();
                }
                else
                {
                    List<int> indices = [];
                    var btns = matches[i].Value.Split(',').Select(int.Parse).ToList();
                    var buttonMask = 0;

                    foreach (int pos in btns)
                    {
                        indices.Add(pos);
                        buttonMask |= 1 << (numberOfLights - 1 - pos);
                    }

                    buttons.Add(new Button(indices, buttonMask));
                }
            }

            var machine = new Machine(numberOfLights, desiredLightMask, buttons, joltages);
            machines.Add(machine);
        }

        return machines;
    }

    [GeneratedRegex("(?<=\\[).*?(?=\\])|(?<=\\().*?(?=\\))|(?<=\\{).*?(?=\\})")]
    private static partial Regex MachineRegex();
}
