namespace AoC2025.Day12;

public sealed record TreeRegion(int Width, int Length, Dictionary<int, int> Presents)
{
    public int Area => Width * Length;
};