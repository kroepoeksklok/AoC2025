namespace AoC2025.Day6;

public sealed class MathProblem
{
    public ICollection<ulong> Input { get; }
    public Operation Operation { get; set; }

    public MathProblem()
    {
        Input = new List<ulong>();
    }
}
