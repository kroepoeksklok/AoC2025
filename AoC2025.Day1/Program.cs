using AoC2025.Day1;

internal class Program
{
    private const int START = 50;

    static void Main()
    {
        Console.WriteLine("Hello, World!");
        var rotations = InputParser.ParseInput();

        SolveA(rotations);
        SolveB(rotations);
    }

    private static void SolveA(IEnumerable<Rotation> rotations)
    {
        var numOfZeroes = 0;
        int currentLocation = START;

        foreach(var rotation in rotations)
        {
            if(rotation.Direction == Direction.Left)
            {
                currentLocation -= rotation.Amount;
            }
            else if (rotation.Direction == Direction.Right)
            {
                currentLocation += rotation.Amount;
            }

            if(currentLocation % 100 == 0)
            {
                numOfZeroes++;
            }
        }

        Console.WriteLine($"1A Answer: {numOfZeroes}");
    }

    private static void SolveB(IEnumerable<Rotation> rotations)
    {
        var zeroPassed = 0;
        int currentLocation = START;
        int previousLocation;

        foreach (var rotation in rotations)
        {
            int amountToRotate;
            if(rotation.Amount >= 100)
            {
                //We're making 1 or more full rotations
                var numberOfRotations = rotation.Amount / 100;
                // Discard full rotations, dial remains in the same spot. Each full rotation passes 0 once.
                amountToRotate = rotation.Amount - (numberOfRotations * 100);
                zeroPassed += numberOfRotations;
            } 
            else
            {
                amountToRotate = rotation.Amount;
            }

            previousLocation = currentLocation;
            if (rotation.Direction == Direction.Left)
            {
                currentLocation -= amountToRotate;
            }
            else if (rotation.Direction == Direction.Right)
            {
                currentLocation += amountToRotate;
            }

            if(currentLocation == 100)
            {
                currentLocation = 0;
                zeroPassed++;
            } 
            else if (currentLocation > 100)
            {
                currentLocation = currentLocation - 100;
                zeroPassed++;
            } 
            else if (currentLocation == 0)
            {
                zeroPassed++;
            }
            else if (currentLocation < 0)
            {
                if(previousLocation > 0) { 
                    zeroPassed++;
                }
                currentLocation = 100 - Math.Abs(currentLocation);
            }
        }

        Console.WriteLine($"1B Answer: {zeroPassed}");
    }
}
