using AoC2025.Day11.Properties;

namespace AoC2025.Day11;

internal static partial class InputParser
{
    public static IEnumerable<Node> ParseInput()
    {
        List<Node> nodes = [];
        var inputString = System.Text.Encoding.UTF8.GetString(Resources.InputDay11a);
        //var inputString =
        //@"aaa: you hhh
        //you: bbb ccc
        //bbb: ddd eee
        //ccc: ddd eee fff
        //ddd: ggg
        //eee: out
        //fff: out
        //ggg: out
        //hhh: ccc fff iii
        //iii: out";
        //        var inputString =
        //@"svr: aaa bbb
        //aaa: fft
        //fft: ccc
        //bbb: tty
        //tty: ccc
        //ccc: ddd eee
        //ddd: hub
        //hub: fff
        //eee: dac
        //dac: fff
        //fff: ggg hhh
        //ggg: out
        //hhh: out";

        var lines = inputString.Split(Environment.NewLine);
        var nodeIndex = 0;

        for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
        {
            var line = lines[lineNumber];
            var parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
            var fromLabel = parts[0].Trim();
            var fromNode = AddNodeIfNotExists(ref nodeIndex, fromLabel, nodes);

            var toLabels = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
            foreach(var toLabel in toLabels)
            {
                var toNode = AddNodeIfNotExists(ref nodeIndex, toLabel, nodes);
                fromNode.AdjacentNodes.Add(toNode);
            }
        }

        return nodes;
    }

    private static Node AddNodeIfNotExists(ref int nodeIndex, string label, List<Node> nodes)
    {
        var existingNode = nodes.FirstOrDefault(n => n.Label == label);
        if (existingNode == null)
        {
            existingNode = new Node(nodeIndex, label);
            nodes.Add(existingNode);
            nodeIndex++;
        }

        return existingNode;
    }
}
