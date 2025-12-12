namespace AoC2025.Day10;

public sealed record Button(IEnumerable<int> ToggledIndices, int Mask);