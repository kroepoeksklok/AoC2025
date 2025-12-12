using AoC2025.Day12.Properties;

namespace AoC2025.Day12;

internal static partial class InputParser
{
    public static Input ParseInput()
    {
        var inputString = System.Text.Encoding.UTF8.GetString(Resources.InputDay12a);

//        var inputString =
//        @"0:
//###
//##.
//##.

//1:
//###
//##.
//.##

//2:
//.##
//###
//##.

//3:
//##.
//###
//##.

//4:
//###
//#..
//###

//5:
//###
//.#.
//###

//4x4: 0 0 0 0 2 0
//12x5: 1 0 1 0 2 2
//12x5: 1 0 1 0 3 2";

        var lines = inputString.Split(Environment.NewLine);

        int presentIndex = 0;
        char[,] form = new char[3, 3];
        int formLine = 0;
        Present present;

        List<Present> presents = [];
        List<TreeRegion> treeRegions = [];

        foreach (var line in lines)
        {
            if(line.Length == 2)
            {
                presentIndex = int.Parse(line.Split(':')[0]);
                formLine = 0;
                form = new char[3, 3];
            } 
            else if (line.Contains('#') || line.Contains('.'))
            {
                for (int i = 0; i < line.Length; i++)
                {
                    form[formLine, i] = line[i];
                }
                formLine++;
            }
            else if (string.IsNullOrWhiteSpace(line))
            {
                present = new Present(presentIndex, form);
                presents.Add(present);
            } 
            else if (line.Contains('x'))
            {
                var parts = line.Split(':');
                var dimensions = parts[0].Split('x');

                int width = int.Parse(dimensions[0]);
                int length = int.Parse(dimensions[1]);

                var numberOfPresents = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Dictionary<int, int> presentCount = [];
                for(int i = 0; i < numberOfPresents.Length; i++)
                {
                    presentCount.Add(i, int.Parse(numberOfPresents[i]));
                }

                var treeRegion = new TreeRegion(width, length, presentCount);
                treeRegions.Add(treeRegion);
            }
        }

        return new Input(presents, treeRegions);
    }
}
