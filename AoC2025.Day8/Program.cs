namespace AoC2025.Day8;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var junctionBoxes = InputParser.ParseInput();

        SolveA(junctionBoxes);
    }

    public static void SolveA(IList<JunctionBox> junctionBoxes)
    {
        List<JunctionBoxDistance> distances = [];

        for (int i = 0; i < junctionBoxes.Count; i++)
        {
            var firstJunctionBox = junctionBoxes[i];
            for (int j = i+1; j < junctionBoxes.Count; j++) 
            {
                var secondJunctionBox = junctionBoxes[j];

                var distance = firstJunctionBox.DistanceTo(secondJunctionBox);
                distances.Add(new JunctionBoxDistance(firstJunctionBox, secondJunctionBox, distance));
            }
        }

        List<Circuit> circuits = new List<Circuit>();
        int connectionsAdded = 0;

        var orderedDistances = distances.OrderBy(d => d.Distance);
        foreach (var orderedDistance in orderedDistances)
        {
            // if (connectionsAdded == 10)
            // {
            //     break;
            // }
            
            Console.WriteLine($"From ({orderedDistance.FirstJunctionBox.X},{orderedDistance.FirstJunctionBox.Y},{orderedDistance.FirstJunctionBox.Z}) to ({orderedDistance.SecondJunctionBox.X},{orderedDistance.SecondJunctionBox.Y},{orderedDistance.SecondJunctionBox.Z}): {orderedDistance.Distance}");

            // bool containsOneJunctionBox = false;
            // bool containsBothJunctionBoxes = false;

            // Circuit containingCircuit = null!;

            // foreach (var circuit in circuits) {
            //     var containsFirstJunctionBox = circuit.ContainsJunctionBox(orderedDistance.FirstJunctionBox);
            //     var containsSecondJunctionBox = circuit.ContainsJunctionBox(orderedDistance.SecondJunctionBox);

            //     if(
            //         (containsFirstJunctionBox && !containsSecondJunctionBox) ||
            //         (!containsFirstJunctionBox && containsSecondJunctionBox)) 
            //     {
            //         containsBothJunctionBoxes = false;
            //         containsOneJunctionBox = true;
            //         containingCircuit = circuit;

            //         break;
            //     }

            //     if(containsFirstJunctionBox && containsSecondJunctionBox) {
            //         containsBothJunctionBoxes = true;
            //         containsOneJunctionBox = false;

            //         break;
            //     }
            // }

            // if(containsBothJunctionBoxes) 
            // {
            //     continue;
            // } 
            // else if(containsOneJunctionBox && containingCircuit != null)
            // {
            //     containingCircuit.JunctionBoxes.Add(orderedDistance.FirstJunctionBox);
            //     containingCircuit.JunctionBoxes.Add(orderedDistance.SecondJunctionBox);
            //     connectionsAdded++;
            // }
            // else 
            // {
            //     containingCircuit = new Circuit();
            //     containingCircuit.JunctionBoxes.Add(orderedDistance.FirstJunctionBox);
            //     containingCircuit.JunctionBoxes.Add(orderedDistance.SecondJunctionBox);
            //     circuits.Add(containingCircuit);
            //     connectionsAdded++;
            // }
        }

        // var orderedCircuits = circuits
        //     .OrderByDescending(c => c.JunctionBoxes.Count);

        // foreach(var circuit in orderedCircuits) {
        //     Console.WriteLine($"{circuit.JunctionBoxes.Count}");
        // }

        // var x = circuits
        //     .OrderByDescending(c => c.JunctionBoxes.Count)
        //     .Select(c => c.JunctionBoxes.Count)
        //     .Take(3)
        //     .Aggregate(1, (a, b) => a * b);

        // // 9614 -> too low
        // Console.WriteLine($"8A answer: {x}");
    }

    public static void SolveB()
    {

    }

    private record JunctionBoxDistance(JunctionBox FirstJunctionBox, JunctionBox SecondJunctionBox, decimal Distance);

    private record Circuit {
        public HashSet<JunctionBox> JunctionBoxes { get; }

        public Circuit() {
            JunctionBoxes = [];
        }

        public bool ContainsJunctionBox(JunctionBox junctionBox) 
        {
            return JunctionBoxes.Any(j => 
                    j.X == junctionBox.X &&  
                    j.Y == junctionBox.Y &&  
                    j.Z == junctionBox.Z);
        }
    }
}
