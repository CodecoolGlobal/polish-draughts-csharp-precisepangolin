using System;

public class Pawn
{
    public string symbol;
    public (int x, int y) coordinates;
    public bool IsCrowned;
    public string color;

    public Pawn(string symbol, (int x, int y) coordinates)
    {
        this.symbol = symbol;
        this.coordinates = coordinates;
        if (symbol == "W")
        {
            color = "white";
        }
        else
        {
            color = "black";
        }
    }

}
