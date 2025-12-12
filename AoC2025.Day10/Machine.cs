namespace AoC2025.Day10;

internal sealed record Machine(int NumberOfLights, int DesiredLightConfiguration, IList<Button> Buttons, IList<int> Joltages);