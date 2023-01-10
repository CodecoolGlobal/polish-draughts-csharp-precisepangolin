using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

public class Board
{
    public string whiteTile = " ##### ";
    public string blackTile = "       ";
    public string whitePawn = "(WHITE)";
    public string blackPawn = "(BLACK)";
    public int n;
    public Pawn[,] Fields;
    public string[,] emptyFields;

    public string[,] GenerateEmptyBoard(int n)
    {
        string[,] emptyFields = new string[n, n];

        // generate black empty fields
        for (int i = 0; i < n; i += 2)
        {
            for (int j = 0; j < n; j += 2)
            {
                emptyFields[i, j] = blackTile;
            }
        }

        for (int i = 1; i < n; i += 2)
        {
            for (int j = 1; j < n; j += 2)
            {
                emptyFields[i, j] = blackTile;
            }
        }

        // generate white empty fields
        for (int i = 1; i < n; i += 2)
        {
            for (int j = 0; j < n; j += 2)
            {
                emptyFields[i, j] = whiteTile;
            }
        }

        for (int i = 0; i < n; i += 2)
        {
            for (int j = 1; j < n; j += 2)
            {
                emptyFields[i, j] = whiteTile;
            }
        }
        return emptyFields;
    }

    public Pawn[,] GenerateBoard(int n)
    {
        Pawn[,] Fields = new Pawn[n, n];

        //int rowsPerPlayer = Convert.ToInt32(Math.Round(Convert.ToDecimal((2 * n) / (n / 2))));
        int rowsPerPlayer = 4;
        if (n <= 8)
        {
            rowsPerPlayer = Convert.ToInt32(Math.Floor(Convert.ToDecimal(n / 2))) - 1;
        }

        // generate black pawns
        for (int i = 0; i < rowsPerPlayer; i += 2)
        {
            for (int j = 0; j < n; j += 2)
            {
                Fields[i, j] = new Pawn(blackPawn, (i, j));
            }
        }

        for (int i = 1; i < rowsPerPlayer; i += 2)
        {
            for (int j = 1; j < n; j += 2)
            {
                Fields[i, j] = new Pawn(blackPawn, (i, j));
            }
        }

        // generate white pawns
        for (int i = n - rowsPerPlayer; i < n; i += 2)
        {
            for (int j = 0; j < n; j += 2)
            {
                Fields[i, j] = new Pawn(whitePawn, (i, j));
            }
        }

        for (int i = n - rowsPerPlayer - 1; i < n; i += 2)
        {
            for (int j = 1; j < n; j += 2)
            {
                Fields[i, j] = new Pawn(whitePawn, (i, j));
            }
        }

        return Fields;
    }

    public void WriteBoard()
    {
        //Console.Clear();
        string writtenBoard = "";
        string writtenBoardRows = "";
        string iLetters = "  ";
        for (int i = 0; i < Fields.GetLength(0); i++)
        {
            string writtenBoardRow = $"{Convert.ToChar(i + 65)} ";
            iLetters += ("   " + (i+1) + "  |");

            for (int j = 0; j < Fields.GetLength(1); j++)
            {
                if (Fields[i, j] != null)
                {
                    writtenBoardRow += Fields[i, j].color;
                }
                else
                {
                    writtenBoardRow += emptyFields[i,j];
                }
            }
            writtenBoardRow += "\n";
            writtenBoardRows += writtenBoardRow;
        }
        iLetters += "\n";
        writtenBoard += iLetters;
        writtenBoard += writtenBoardRows;

        Console.Write(writtenBoard);
    }

    public string @ToString((int i, int j) coordinates)
    {
        string textCoordinates;
        string letter = (Convert.ToChar(coordinates.i + 65)).ToString();
        string digit = (coordinates.j + 1).ToString();
        return letter + digit;
    }

    public (int i, int j) @ToCoordinates(string input)
    {
        (int i, int j) coordinates;
        char letter = Convert.ToChar(input[0].ToString().ToUpper());
        int letterValue = (int)letter;
        int i = Convert.ToInt32(letterValue) - 65;
        int j = 0;
        Console.WriteLine(input.Length);
        if (input.Length > 2)
        {
            string digits = input[1].ToString() + input[2].ToString();
            Console.WriteLine(digits);
            Int32.TryParse(digits, out j);
        }
        else {
            Int32.TryParse(input[1].ToString(), out j);
        }
        j -= 1;
        coordinates = (i, j);
        return coordinates;
    }

    public void @RemovePawn((int i, int j) coordinates)
    {
        int i = coordinates.i;
        int j = coordinates.j;
        if (Fields[i, j] != null)
        {
            string removedColor = Fields[i, j].color;
            Fields[i, j] = null;
            Console.WriteLine($"Removed {removedColor} pawn from {@ToString(coordinates)}");
        }
        else
        {
            Console.WriteLine($"No pawns at ({i},{j})");
        }
    }

    public void @MovePawn((int i, int j) coordinatesA, (int k, int l) coordinatesB)
    {
        int i = coordinatesA.i;
        int j = coordinatesA.j;
        int k = coordinatesB.k;
        int l = coordinatesB.l;
        if (Fields[i,j] != null && Fields[k,l] == null)
        {
            string pawnColor = Fields[i, j].color;
            Fields[i, j] = null;
            Fields[k, l] = new Pawn(pawnColor, (k, l));
            Console.WriteLine($"Moved {pawnColor} pawn from {@ToString(coordinatesA)} to {@ToString(coordinatesB)}.");
            
            if (Fields[(i+k)/2, (j+l)/2] != null && Fields[(i + k) / 2, (j + l) / 2].color != pawnColor)
            {
                @RemovePawn(((i + k) / 2, (j + l) / 2));
            }
        }
        else
        {
            Console.WriteLine($"Cannot move pawn from {@ToString(coordinatesA)} to {@ToString(coordinatesB)}.");
        }
    }

    public Board(int n)
    {
        this.n = n;
        this.Fields = GenerateBoard(n);
        this.emptyFields = GenerateEmptyBoard(n);
    }
}
