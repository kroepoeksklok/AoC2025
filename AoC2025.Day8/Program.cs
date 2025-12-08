namespace AoC2025.Day8;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        var junctionBoxes = InputParser.ParseInput();

        SolveA(junctionBoxes);
        SolveB(junctionBoxes);
    }

    public static void SolveA(IList<JunctionBox> junctionBoxes)
    {
        List<JunctionBoxDistance> orderedDistances = CalculateAllDistances(junctionBoxes);
        List<Circuit> circuits = [];
        int connectionsAdded = 0;

        foreach (var orderedDistance in orderedDistances)
        {
            if (connectionsAdded == 1000)
            {
                break;
            }

            var containingCircuits = GetCircuitsThatContainAtLeastOneJunctionBox(circuits, orderedDistance);

            if (containingCircuits.Count == 0)
            {
                CreateNewCircuit(circuits, orderedDistance);
                connectionsAdded++;
            }
            else if (containingCircuits.Count == 1)
            {
                AddJunctionBoxesToContainingCircuit(orderedDistance, containingCircuits);
                connectionsAdded++;
            }
            else
            {
                MergeCircuits(circuits, orderedDistance, containingCircuits);

                connectionsAdded++;
            }
        }

        var x = circuits
            .OrderByDescending(c => c.JunctionBoxes.Count)
            .Select(c => c.JunctionBoxes.Count)
            .Take(3)
            .Aggregate(1, (a, b) => a * b);

        Console.WriteLine($"8A answer: {x}");
    }

    public static void SolveB(IList<JunctionBox> junctionBoxes)
    {
        List<JunctionBoxDistance> orderedDistances = CalculateAllDistances(junctionBoxes);
        List<Circuit> circuits = [];

        foreach (var orderedDistance in orderedDistances)
        {
            var containingCircuits = GetCircuitsThatContainAtLeastOneJunctionBox(circuits, orderedDistance);

            if (containingCircuits.Count == 0)
            {
                CreateNewCircuit(circuits, orderedDistance);
            }
            else if (containingCircuits.Count == 1)
            {
                AddJunctionBoxesToContainingCircuit(orderedDistance, containingCircuits);
            }
            else
            {
                MergeCircuits(circuits, orderedDistance, containingCircuits);
            }

            var circuitWithAllBoxes = circuits.FirstOrDefault(c => c.JunctionBoxes.Count == junctionBoxes.Count);
            if (circuitWithAllBoxes != null)
            {
                var answer = (long)orderedDistance.FirstJunctionBox.X * orderedDistance.SecondJunctionBox.X;

                Console.WriteLine($"8B answer: {answer}");
                break;
            }
        }
    }

    private static List<JunctionBoxDistance> CalculateAllDistances(IList<JunctionBox> junctionBoxes)
    {
        List<JunctionBoxDistance> distances = [];
        for (int i = 0; i < junctionBoxes.Count; i++)
        {
            var firstJunctionBox = junctionBoxes[i];
            for (int j = i + 1; j < junctionBoxes.Count; j++)
            {
                var secondJunctionBox = junctionBoxes[j];

                var distance = firstJunctionBox.DistanceTo(secondJunctionBox);
                distances.Add(new JunctionBoxDistance(firstJunctionBox, secondJunctionBox, distance));
            }
        }

        return distances.OrderBy(d => d.Distance).ToList();
    }

    private static List<Circuit> GetCircuitsThatContainAtLeastOneJunctionBox(List<Circuit> circuits, JunctionBoxDistance orderedDistance)
    {
        List<Circuit> containingCircuits = [];
        foreach (var circuit in circuits.OrderByDescending(c => c.JunctionBoxes.Count))
        {
            var containsFirstJunctionBox = circuit.ContainsJunctionBox(orderedDistance.FirstJunctionBox);
            var containsSecondJunctionBox = circuit.ContainsJunctionBox(orderedDistance.SecondJunctionBox);

            if (containsFirstJunctionBox || containsSecondJunctionBox)
            {
                containingCircuits.Add(circuit);
            }
        }

        return containingCircuits;
    }

    private static void CreateNewCircuit(List<Circuit> circuits, JunctionBoxDistance orderedDistance)
    {
        var circuit = new Circuit();
        circuit.JunctionBoxes.Add(orderedDistance.FirstJunctionBox);
        circuit.JunctionBoxes.Add(orderedDistance.SecondJunctionBox);
        circuits.Add(circuit);
    }

    private static void AddJunctionBoxesToContainingCircuit(JunctionBoxDistance orderedDistance, List<Circuit> containingCircuits)
    {
        var containingCircuit = containingCircuits[0];
        containingCircuit.JunctionBoxes.Add(orderedDistance.FirstJunctionBox);
        containingCircuit.JunctionBoxes.Add(orderedDistance.SecondJunctionBox);
    }

    private static void MergeCircuits(List<Circuit> circuits, JunctionBoxDistance orderedDistance, List<Circuit> containingCircuits)
    {
        var newCircuit = new Circuit();
        foreach (var containingCircuit in containingCircuits)
        {
            foreach (var junctionBox in containingCircuit.JunctionBoxes)
            {
                newCircuit.JunctionBoxes.Add(junctionBox);
            }
            circuits.Remove(containingCircuit);
        }

        newCircuit.JunctionBoxes.Add(orderedDistance.FirstJunctionBox);
        newCircuit.JunctionBoxes.Add(orderedDistance.SecondJunctionBox);
        circuits.Add(newCircuit);
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
