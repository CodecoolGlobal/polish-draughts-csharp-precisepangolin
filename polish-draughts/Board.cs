using System;
using System.Diagnostics;
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
        if (Process.GetCurrentProcess().MainWindowTitle != "")
        {
            whitePawn = "wh";
        }

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
        string letter = (Convert.ToChar(coordinates.i + 65)).ToString();
        string digit = (coordinates.j + 1).ToString();
        return letter + digit;
    }

    public (int i, int j) @ToCoordinates(string input)
    {
        (int i, int j) coordinates;
        char letter;
        try
        {  letter = Convert.ToChar(input[0].ToString().ToUpper()); }
        catch
        {
            letter = 'A';
        };
        int letterValue = (int)letter;
        int i = Convert.ToInt32(letterValue) - 65;
        int j;
        if (input.Length == 3)
        {
            string digits = input[1].ToString() + input[2].ToString();
            Int32.TryParse(digits, out j);
        }
        else {
            try
            {
                Int32.TryParse(input[1].ToString(), out j);
            }
            catch
            {
                j = 0;
            }
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
            string verbalRemovedColor;
            if (removedColor == whitePawn)
            {
                verbalRemovedColor = "white";
            }
            else
            {
                verbalRemovedColor = "black";
            }
            Fields[i, j] = null;
            Console.WriteLine($"Removed {verbalRemovedColor} pawn from {@ToString(coordinates)}");
        }
        else
        {
            Console.WriteLine($"No pawns at ({i},{j})");
        }
    }

    public bool @MovePawn((int i, int j) coordinatesA, (int k, int l) coordinatesB, string pawnColor)
    {
        int i = coordinatesA.i;
        int j = coordinatesA.j;
        int k = coordinatesB.k;
        int l = coordinatesB.l;
        string verbalPawnColor;
        if (pawnColor == whitePawn)
        { 
            verbalPawnColor = "white"; }
        else
        {
            verbalPawnColor = "black";
        };

        if (Fields[i,j] != null && Fields[i, j].color != pawnColor)
        {

            Console.WriteLine($"Please select a {verbalPawnColor} pawn!");
            return false;
        }
        if (Fields[i,j] != null && Fields[k,l] == null && Math.Abs(i - k) == Math.Abs(j - l))
        {
            Pawn inBetween = Fields[(i + k) / 2, (j + l) / 2];


            if (Math.Abs(i - k) == 1 || (Math.Abs(i - k) == 2 && inBetween != null && inBetween.color != pawnColor))
            {
                Fields[i, j] = null;
                Fields[k, l] = new Pawn(pawnColor, (k, l));
                Console.WriteLine($"Moved {verbalPawnColor} pawn from {@ToString(coordinatesA)} to {@ToString(coordinatesB)}.");
                if (Math.Abs(i-k) == 2)
                {
                    @RemovePawn(((i + k) / 2, (j + l) / 2));
                }
                return true;
            }
            else
            {
                Console.WriteLine($"Cannot move pawn from {@ToString(coordinatesA)} to {@ToString(coordinatesB)}." +
                    $" \n You cannot jump over your own pawns.");
                return false;
            }
            
        }
        else
        {
            Console.WriteLine($"Cannot move pawn from {@ToString(coordinatesA)} to {@ToString(coordinatesB)}." +
                $" \nMake sure to move diagonally and that the place is not occupied.");
            return false;
        }
    }

    public Board(int n)
    {
        this.n = n;
        this.Fields = GenerateBoard(n);
        this.emptyFields = GenerateEmptyBoard(n);
    }
}
