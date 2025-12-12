namespace AoC2025.Day12;

public sealed record Present(int Id, char[,] Shape)
{
    private int? area;
    public int Area => area ??= Shape.Cast<char>().Count(c => c == '#');
};
