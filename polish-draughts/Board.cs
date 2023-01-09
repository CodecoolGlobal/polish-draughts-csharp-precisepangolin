using System;

public class Board
{
    public int n;
    public Pawn[,] Fields;

    public Pawn[,] GenerateBoard(int n)
    {
        Pawn[,] Fields = new Pawn[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j += 2)
            {
                Fields[i, j] = new Pawn("white", (i, j));
            }
            for (int j = 1; j < n; j += 2)
            {
                //Fields[i, j] = new Pawn("black", (i, j));
            }
        }
        return Fields;
    }

    public void WriteBoard()
    {
        string writtenBoard = "";
        for (int i = 0; i < Fields.GetLength(0); i++)
        {
            string writtenBoardRow = "";

            for (int j = 0; j < Fields.GetLength(1); j++)
            {
                if (Fields[i, j] != null)
                {
                    writtenBoardRow += Fields[i, j].color;
                }
                else
                {
                    writtenBoardRow += "null";
                }
            }
            writtenBoardRow += "\n";
            writtenBoard += writtenBoardRow;
        }
        Console.Write(writtenBoard);
    }

    public override string @ToString()
    {
        return base.ToString() + ": (@ToString method)";
    }

    public void @RemovePawn((int i, int j) coordinates)
    {
        Console.WriteLine($"Remove pawn from {coordinates}");
    }

    public void @MovePawn((int i, int j) coordinatesA, (int k, int l) coordinatesB)
    {
        Console.WriteLine($"Move pawn from {coordinatesA} to {coordinatesB}");
    }

    public Board(int n)
    {
        this.n = n;
        this.Fields = GenerateBoard(n);
    }
}
