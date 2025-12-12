namespace AoC2025.Day8;

public sealed record JunctionBox(int X, int Y, int Z)
{
    public decimal DistanceTo(JunctionBox junctionBox)
    {
        var xCoordinate = Math.Pow(X - junctionBox.X, 2);
        var yCoordinate = Math.Pow(Y - junctionBox.Y, 2);
        var zCoordinate = Math.Pow(Z - junctionBox.Z, 2);
        var distance = Math.Sqrt(xCoordinate + yCoordinate + zCoordinate);

        return (decimal) distance;
    }
};