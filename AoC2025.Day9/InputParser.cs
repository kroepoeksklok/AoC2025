using AoC2025.Day9.Properties;

namespace AoC2025.Day9;

internal static class InputParser
{
    public static CoordinatesAndEdges ParseInput()
    {

        var inputString = System.Text.Encoding.UTF8.GetString(Resources.InputDay9a);
//        var inputString =
//@"7,1
//11,1
//11,7
//9,7
//9,5
//2,5
//2,3
//7,3";
        var coordinates = new List<Coordinate>();
        var edges = new List<Edge>();

        var lines = inputString.Split(Environment.NewLine);

        for (int i = 0; i < lines.Length; i++)
        {
            var iCurrentCoordinate = lines[i];
            string iNextCoordinate = i == lines.Length - 1 ? 
                lines[0] : 
                lines[i + 1];

            var currentCoordinate = ParseLine(iCurrentCoordinate);
            var nextCoordinate = ParseLine(iNextCoordinate);

            edges.Add(new Edge(currentCoordinate, nextCoordinate));
            coordinates.Add(currentCoordinate);
        }

        return new CoordinatesAndEdges(coordinates, edges);
    }

    private static Coordinate ParseLine(string line)
    {
        var boundary = line.Split(',');
        return new Coordinate(long.Parse(boundary[0]), long.Parse(boundary[1]));
    }
}
