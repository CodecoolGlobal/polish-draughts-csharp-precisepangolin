using System;

public class Pawn
{
    public string color;
    public (int x, int y) coordinates;
    public bool IsCrowned;

    public Pawn(string color, (int x, int y) coordinates)
    {
        this.color = color;
        this.coordinates = coordinates;
    }

}
