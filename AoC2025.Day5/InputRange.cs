using System.Diagnostics;

namespace AoC2025.Day5;

[DebuggerDisplay("{Start}, {End}")]
internal sealed class InputRange(ulong start, ulong end)
{
    public ulong Start { get; set; } = start;
    public ulong End { get; set; } = end;
}
