namespace AoC2025.Day9;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var coordinates = InputParser.ParseInput();

        SolveA(coordinates);
        SolveB(coordinates);
    }

    private static void SolveA(IList<Coordinate> coordinates)
    {
        var surfaces = CreateSurfaces(coordinates);
        var largestSurfaceArea = surfaces.OrderByDescending(s => s.Area).First().Area;
        Console.WriteLine($"Answer 9A: {largestSurfaceArea}");
    }

    private static void SolveB(IList<Coordinate> coordinates)
    {
        var surfaces = CreateSurfaces(coordinates);
        Surface largestSurfaceAreaWithoutReds = null;

        foreach (var surface in surfaces.OrderByDescending(s => s.Area))
        {
            bool surfaceAreaInnerPartHasReds = true;
            if (surface.FirstCorner.Y == surface.OppositeCorner.Y)
            {
                // straight horizontal line
                var smallestX = Math.Min(surface.FirstCorner.X, surface.OppositeCorner.X);
                var largestX = Math.Max(surface.FirstCorner.X, surface.OppositeCorner.X);

                surfaceAreaInnerPartHasReds = coordinates.Any(c =>
                    (c.X >= smallestX + 1 && c.X <= largestX - 1) &&
                    c.Y == surface.FirstCorner.Y);
            } 
            else if (surface.FirstCorner.X == surface.OppositeCorner.X)
            {
                // Straight vertical line
                var smallestY = Math.Min(surface.FirstCorner.Y, surface.OppositeCorner.Y);
                var largestY = Math.Max(surface.FirstCorner.Y, surface.OppositeCorner.Y);

                surfaceAreaInnerPartHasReds = coordinates.Any(c =>
                    (c.Y >= smallestY + 1 && c.Y <= largestY - 1) &&
                    (c.X == surface.FirstCorner.X)
                );
            } 
            else
            {
                var smallestX = Math.Min(surface.FirstCorner.X, surface.OppositeCorner.X) + 1;
                var smallestY = Math.Min(surface.FirstCorner.Y, surface.OppositeCorner.Y) + 1;
                var largestX = Math.Max(surface.FirstCorner.X, surface.OppositeCorner.X) - 1;
                var largestY = Math.Max(surface.FirstCorner.Y, surface.OppositeCorner.Y) - 1;

                var coords = coordinates.Where(c =>
                    (c.Y >= smallestY && c.Y <= largestY) &&
                    (c.X >= smallestX && c.X <= largestX)
                );

                ;
            }

            if (!surfaceAreaInnerPartHasReds)
            {
                largestSurfaceAreaWithoutReds = surface;
                break;
            }
        }

        Console.WriteLine($"Answer 9B: {largestSurfaceAreaWithoutReds.Area}");
    }

    private static IEnumerable<Surface> CreateSurfaces(IList<Coordinate> coordinates)
    {
        List<Surface> surfaces = [];
        for (int i = 0; i < coordinates.Count; i++)
        {
            var firstCoordinate = coordinates[i];
            for (int j = i + 1; j < coordinates.Count; j++)
            {
                var secondCoordinate = coordinates[j];
                var surface = new Surface(firstCoordinate, secondCoordinate);
                surfaces.Add(surface);
            }
        }

        return surfaces;
    }

    private record Surface(Coordinate FirstCorner, Coordinate OppositeCorner)
    {
        public decimal Area => 
            (Math.Abs((decimal)FirstCorner.X - OppositeCorner.X) + 1) *
            (Math.Abs((decimal)FirstCorner.Y - OppositeCorner.Y) + 1);
    };
}
