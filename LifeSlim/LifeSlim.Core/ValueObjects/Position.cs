namespace LifeSlim.Core.ValueObjects;

public class Position
{
    public int Y { get; set; }
    public int X { get; set; }

    public Position(int x, int y)
    {
        Y = y;
        X = x;
    }
}