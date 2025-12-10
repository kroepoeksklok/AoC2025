namespace AoC2025.Day9;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        var coordinatesAndEdges = InputParser.ParseInput();

        var surfaces = CreateSurfaces(coordinatesAndEdges.Coordinates);
        SolveA(surfaces);
        SolveB(surfaces, coordinatesAndEdges.Edges);
    }

    private static void SolveA(IEnumerable<Surface> surfaces)
    {
        var largestSurfaceArea = surfaces.OrderByDescending(s => s.Area).First();
        Console.WriteLine($"Answer 9A: {largestSurfaceArea.Area}");
    }

    private static void SolveB(IEnumerable<Surface> surfaces, IList<Edge> edges)
    {
        Surface largestSurfaceArea = null;

        foreach (var surface in surfaces.OrderByDescending(s => s.Area))
        {
            bool allCornersInPolygon = true;
            foreach(var corner in surface.Corners)
            {
                allCornersInPolygon &= IsPointInPolygon(corner, edges);
                if (!allCornersInPolygon) { 
                    break;
                }
            }

            if(!allCornersInPolygon)
            {
                continue;
            }

            // all corners are in the polygon
            var edgesIntersect = false;
            foreach (var surfaceEdge in surface.Edges)
            {
                foreach(var edge in edges)
                {
                    edgesIntersect = DoEdgesIntersect(edge, surfaceEdge);
                    if (edgesIntersect)
                    {
                        break;
                    }
                }

                if(edgesIntersect)
                {
                    break;
                }
            }

            if (edgesIntersect) 
            {
                continue;
            }

            largestSurfaceArea = surface;
            break;
        }

        Console.WriteLine($"Answer 9B: {largestSurfaceArea.Area}");
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

    private static bool IsPointInPolygon(Coordinate coordinate, IEnumerable<Edge> edges)
    {
        foreach (var edge in edges)
        {
            if(edge.FirstCoordinate.Y == edge.SecondCoordinate.Y)
            {
                // Horizontal
                if(coordinate.Y == edge.FirstCoordinate.Y && 
                   Math.Min(edge.FirstCoordinate.X, edge.SecondCoordinate.X) <= coordinate.X &&
                   coordinate.X <= Math.Max(edge.FirstCoordinate.X, edge.SecondCoordinate.X))
                {
                    return true;
                }
            } 
            else if (edge.FirstCoordinate.X == edge.SecondCoordinate.X)
            {
                // Vertical
                if (coordinate.X == edge.FirstCoordinate.X &&
                   Math.Min(edge.FirstCoordinate.Y, edge.SecondCoordinate.Y) <= coordinate.Y &&
                   coordinate.Y <= Math.Max(edge.FirstCoordinate.Y, edge.SecondCoordinate.Y))
                {
                    return true;
                }
            }
        }

        bool inside = false;
        foreach(var edge in edges)
        {
            if(edge.FirstCoordinate.Y == edge.SecondCoordinate.Y)
            {
                continue;
            }

            if(coordinate.Y >= Math.Min(edge.FirstCoordinate.Y, edge.SecondCoordinate.Y) && 
                coordinate.Y < Math.Max(edge.FirstCoordinate.Y, edge.SecondCoordinate.Y))
            {
                if(edge.FirstCoordinate.X > coordinate.X)
                {
                    inside = !inside;
                }
            }
        }

        return inside;
    }

    private static bool DoEdgesIntersect(Edge polygonEdge, Edge surfaceEdge)
    {
        var pVertical = polygonEdge.FirstCoordinate.X == polygonEdge.SecondCoordinate.X;
        var pHorizontal = polygonEdge.FirstCoordinate.Y == polygonEdge.SecondCoordinate.Y;
        var rVertical = surfaceEdge.FirstCoordinate.X == surfaceEdge.SecondCoordinate.X;
        var rHorizontal = surfaceEdge.FirstCoordinate.Y == surfaceEdge.SecondCoordinate.Y;

        if (pVertical && rHorizontal)
        {
            // Vertical polygon edge, horizontal rectangle edge
            var vX = polygonEdge.FirstCoordinate.X;
            var hY = surfaceEdge.FirstCoordinate.Y;

            var hXmin = Math.Min(surfaceEdge.FirstCoordinate.X, surfaceEdge.SecondCoordinate.X);
            var hXmax = Math.Max(surfaceEdge.FirstCoordinate.X, surfaceEdge.SecondCoordinate.X);
            var vYmin = Math.Min(polygonEdge.FirstCoordinate.Y, polygonEdge.SecondCoordinate.Y);
            var vYmax = Math.Max(polygonEdge.FirstCoordinate.Y, polygonEdge.SecondCoordinate.Y);

            // proper interior crossing only (strict)
            var xCross = vX > hXmin && vX < hXmax;
            var yCross = hY > vYmin && hY < vYmax;

            return xCross && yCross;
        }
        else if (pHorizontal && rVertical)
        {
            // Horizontal polygon edge, vertical rectangle edge
            var hY = polygonEdge.FirstCoordinate.Y;
            var vX = surfaceEdge.FirstCoordinate.X;

            var hXmin = Math.Min(polygonEdge.FirstCoordinate.X, polygonEdge.SecondCoordinate.X);
            var hXmax = Math.Max(polygonEdge.FirstCoordinate.X, polygonEdge.SecondCoordinate.X);
            var vYmin = Math.Min(surfaceEdge.FirstCoordinate.Y, surfaceEdge.SecondCoordinate.Y);
            var vYmax = Math.Max(surfaceEdge.FirstCoordinate.Y, surfaceEdge.SecondCoordinate.Y);

            var xCross = vX > hXmin && vX < hXmax;
            var yCross = hY > vYmin && hY < vYmax;

            return xCross && yCross;
        }

        // --- Parallel edges (vertical ↔ vertical or horizontal ↔ horizontal) ---
        // Overlaps or touching do NOT count as intersections for rectangle containment
        return false;
    }

    private record Surface(Coordinate FirstCorner, Coordinate OppositeCorner)
    {
        public IEnumerable<Coordinate> Corners =>
        [
                FirstCorner,
                new Coordinate(OppositeCorner.X, FirstCorner.Y),
                OppositeCorner,
                new Coordinate(FirstCorner.X, OppositeCorner.Y)
        ];

        public decimal Area => 
            (Math.Abs(FirstCorner.X - OppositeCorner.X) + 1) *
            (Math.Abs(FirstCorner.Y - OppositeCorner.Y) + 1);

        public IEnumerable<Edge> Edges =>
        [
            new Edge(FirstCorner, new Coordinate(OppositeCorner.X, FirstCorner.Y)),
            new Edge(new Coordinate(OppositeCorner.X, FirstCorner.Y), OppositeCorner),
            new Edge(OppositeCorner, new Coordinate(FirstCorner.X, OppositeCorner.Y)),
            new Edge(new Coordinate(FirstCorner.X, OppositeCorner.Y), FirstCorner),
        ];
    };
}
