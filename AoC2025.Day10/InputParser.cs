using AoC2025.Day10.Properties;
using System;
using System.ComponentModel;
using System.Text;

namespace AoC2025.Day10;

internal static class InputParser
{
    public static object ParseInput()
    {

        //var inputString = System.Text.Encoding.UTF8.GetString(Resources.InputDay9a);
        var inputString =
@"[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}";

        var machines = new List<Machine>();

        var lines = inputString.Split(Environment.NewLine);

        //for (int i = 0; i < lines.Length; i++)
        //{
        //    var machine = new Machine([], []);
        //    machines.Add(machine);

        //    var line = lines[i];
        //    var lightIndex = 0;

        //    foreach(var c in line)
        //    {
        //        if (c == ' ' || c == ']') continue;
        //        if (c == '[')
        //        {
        //            lightIndex = 0;
        //        }
        //        if( c == '.')
        //        {
        //            machine.DesiredConfiguration.Add(new LightConfiguration(i, State.Off));
        //            lightIndex++;
        //        }
        //        if (c == '#')
        //        {
        //            machine.DesiredConfiguration.Add(new LightConfiguration(i, State.On));
        //            lightIndex++;
        //        }
        //        if(c == '(')
        //    }
        //    var iCurrentCoordinate = lines[i];
        //    string iNextCoordinate = i == lines.Length - 1 ? 
        //        lines[0] : 
        //        lines[i + 1];

        //    var currentCoordinate = ParseLine(iCurrentCoordinate);
        //    var nextCoordinate = ParseLine(iNextCoordinate);

        //    edges.Add(new Edge(currentCoordinate, nextCoordinate));
        //    coordinates.Add(currentCoordinate);
        //}

        //return new CoordinatesAndScanLines(coordinates, edges);
        return null;
    }
}

internal sealed record Machine
{
    public IList<LightConfiguration> DesiredConfiguration { get; }
    public IList<Button> Buttons { get; }
    public IList<LightConfiguration> CurrentConfiguration { get; }

    public Machine(IList<LightConfiguration> desiredConfiguration, IList<Button> buttons)
    {
        DesiredConfiguration = desiredConfiguration;
        Buttons = buttons;

        CurrentConfiguration = [];
        for (var i = 0; 0 < DesiredConfiguration.Count; i++)
        {
            CurrentConfiguration.Add(new LightConfiguration(i, State.Off));
        }
    }
}

internal sealed record LightConfiguration(int Index, State State);

internal enum State
{
    Off = 0,
    On = 1
}

internal sealed record Button(IEnumerable<int> Indices);