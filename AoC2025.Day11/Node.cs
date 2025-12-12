namespace AoC2025.Day11;

public sealed record Node(int Index, string Label)
{
    public List<Node> AdjacentNodes { get; } = [];
}