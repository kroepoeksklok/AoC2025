using AoC2025.Day8.Properties;
using System.Text;

namespace AoC2025.Day8;

internal static class InputParser
{
    public static IList<JunctionBox> ParseInput()
    {

        var inputString = System.Text.Encoding.UTF8.GetString(Resources.InputDay8a);
//        var inputString =
//@"162,817,812
//57,618,57
//906,360,560
//592,479,940
//352,342,300
//466,668,158
//542,29,236
//431,825,988
//739,650,466
//52,470,668
//216,146,977
//819,987,18
//117,168,530
//805,96,715
//346,949,466
//970,615,88
//941,993,340
//862,61,35
//984,92,344
//425,690,689";

        var junctionBoxes = new List<JunctionBox>();
        var lines = inputString.Split(Environment.NewLine);
        foreach(var line in lines)
        {
            var coordinates = line.Split(',');
            var junctionBox = new JunctionBox(int.Parse(coordinates[0]), int.Parse(coordinates[1]), int.Parse(coordinates[2]));
            junctionBoxes.Add(junctionBox);
        }

        return junctionBoxes;
    }
}

public sealed record JunctionBox(int X, int Y, int Z)
{
    public decimal DistanceTo(JunctionBox junctionBox)
    {
        var xCoordinate = Math.Pow((X - junctionBox.X), 2);
        var yCoordinate = Math.Pow((Y - junctionBox.Y), 2);
        var zCoordinate = Math.Pow((Z - junctionBox.Z), 2);
        var distance = Math.Sqrt(xCoordinate + yCoordinate + zCoordinate);

        return (decimal) distance;
    }
};