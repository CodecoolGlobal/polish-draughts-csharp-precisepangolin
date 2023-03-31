using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

public class Board
{
    public string whiteTile = "w";
    public string blackTile = "b";
    public string whitePawn = "W";
    public string blackPawn = "V";
    public int n;
    public Pawn[,] Fields;
    public string[,] emptyFields;

    public string[,] GenerateEmptyBoard(int n)
    {
        string[,] emptyFields = new string[n, n];

        // generate white empty fields
        for (int i = 0; i < n; i += 2)
        {
            for (int j = 0; j < n; j += 2)
            {
                emptyFields[i, j] = whiteTile;
            }
        }

        for (int i = 1; i < n; i += 2)
        {
            for (int j = 1; j < n; j += 2)
            {
                emptyFields[i, j] = whiteTile;
            }
        }

        // generate black empty fields
        for (int i = 1; i < n; i += 2)
        {
            for (int j = 0; j < n; j += 2)
            {
                emptyFields[i, j] = blackTile;
            }
        }

        for (int i = 0; i < n; i += 2)
        {
            for (int j = 1; j < n; j += 2)
            {
                emptyFields[i, j] = blackTile;
            }
        }
        return emptyFields;
    }

    public Pawn[,] GenerateBoard(int n)
    {
        Pawn[,] Fields = new Pawn[n, n];
        int rowsPerPlayer = 4;
        if (n <= 8)
        {
            rowsPerPlayer = Convert.ToInt32(Math.Floor(Convert.ToDecimal(n / 2))) - 1;
        }

        // generate black pawns
        for (int i = 0; i < rowsPerPlayer; i += 2)
        {
            for (int j = 1; j < n; j += 2)
            {
                Fields[i, j] = new Pawn(blackPawn, (i, j));
            }
        }
        for (int i = 1; i < rowsPerPlayer; i += 2)
        {
            for (int j = 0; j < n; j += 2)
            {
                Fields[i, j] = new Pawn(blackPawn, (i, j));
            }
        }
        // generate white pawns
        for (int i = n - rowsPerPlayer + 1; i < n; i += 2)
        {
            for (int j = 0; j < n; j += 2)
            {
                Fields[i, j] = new Pawn(whitePawn, (i, j));
            }
        }

        for (int i = n - rowsPerPlayer; i < n; i += 2)
        {
            for (int j = 1; j < n; j += 2)
            {
                Fields[i, j] = new Pawn(whitePawn, (i, j));
            }
        }

        return Fields;
    }

    public string ToString((int i, int j) coordinates)
    {
        string letter = (Convert.ToChar(coordinates.i + 65)).ToString();
        string digit = (coordinates.j + 1).ToString();
        return letter + digit;
    }

    public (int i, int j) ToCoordinates(string input)
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

    public void RemovePawn((int i, int j) coordinates)
    {
        int i = coordinates.i;
        int j = coordinates.j;
        if (Fields[i, j] != null)
        {
            string removedColor = Fields[i, j].color;
            Fields[i, j] = null;
            Console.WriteLine($"Removed {removedColor} pawn from {ToString(coordinates)}");
        }
    }

    public bool IsDiagonal((int i, int j) coordinatesA, (int k, int l) coordinatesB)
    {
        int i = coordinatesA.i;
        int j = coordinatesA.j;
        int k = coordinatesB.k;
        int l = coordinatesB.l;
        if (Fields[i, j] != null && Fields[k, l] == null && Math.Abs(i - k) == Math.Abs(j - l))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsValidMove((int i, int j) coordinatesA, (int k, int l) coordinatesB)
    {
        if (!IsDiagonal(coordinatesA, coordinatesB))
        {
            return false;
        }
        int i = coordinatesA.i;
        int j = coordinatesA.j;
        int k = coordinatesB.k;
        int l = coordinatesB.l;
        Pawn inBetween = Fields[(i + k) / 2, (j + l) / 2];
        if (Math.Abs(i - k) == 1 || (Math.Abs(i - k) == 2 && inBetween != null && inBetween.color != Fields[i,j].color))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MovePawn(Pawn pawn, (int k, int l) coordinatesB)
    {
        int i = pawn.coordinates.x;
        int j = pawn.coordinates.y;
        int k = coordinatesB.k;
        int l = coordinatesB.l;
        Pawn inBetween = Fields[(i + k) / 2, (j + l) / 2];
        Fields[coordinatesB.k, coordinatesB.l] = new Pawn(pawn.symbol, coordinatesB);
        Fields[i, j] = null;
        if (inBetween != null)
        {
            RemovePawn(inBetween.coordinates);
        }
    }

    public List<(int i, int j)> SuggestedPawn(string pawnColor)
    {
        List<Pawn> pawns = new List<Pawn>();
        List<(int i, int j)> coordinates = new List<(int i, int j)>();
        foreach (Pawn pawn in Fields)
        {
            if (pawn != null && pawn.color == pawnColor)
            {
                for (int i = 0; i < Fields.GetLength(0); i++)
                {
                    for (int j = 0; j < Fields.GetLength(1); j++)
                    {
                        if (IsValidMove(pawn.coordinates, (i, j)) && !pawns.Contains(pawn))
                        {
                            pawns.Add(pawn);
                        }
                    }
                }
            }
        }
        foreach (Pawn pawn in pawns)
        {
            coordinates.Add(pawn.coordinates);
        }
        return coordinates;
    }

    public List<(int i, int j)> SuggestedMove((int i, int j) coordinatesA)
    {
        List<(int i, int j)> suggestedMoves = new List<(int i, int j)>();
        for (int i = 0; i < Fields.GetLength(0); i++)
        {
            for (int j = 0; j < Fields.GetLength(1); j++)
            {
                if (IsValidMove(coordinatesA, (i,j)))
                {
                    suggestedMoves.Add((i,j));
                }
            }

        }
        return suggestedMoves;
    }

    public bool CanJumpOver((int i, int j) coordinatesA, (int k, int l) coordinatesB, string pawnSymbol)
    {
        int i = coordinatesA.i;
        int j = coordinatesA.j;
        int k = coordinatesB.k;
        int l = coordinatesB.l;
        Pawn inBetween = Fields[(i + k) / 2, (j + l) / 2];
        if (inBetween != null && inBetween.symbol != pawnSymbol && Math.Abs(i - k) == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool WillBeJumped((int i, int j) coordinatesA, (int k, int l) coordinatesB, string pawnColor)
    {
        string pawnSymbol;
        string enemyPawnSymbol;
        bool jumpedFlag = false;
        bool deletedFlag = false;
        if (pawnColor == "white")
        {
            pawnSymbol = whitePawn;
            enemyPawnSymbol = blackPawn;
        }
        else
        {
            pawnSymbol = blackPawn;
            enemyPawnSymbol = whitePawn;
        }
        int i = coordinatesA.i;
        int j = coordinatesA.j;
        int k = coordinatesB.k;
        int l = coordinatesB.l;
        Pawn inBetween = Fields[(i + k) / 2, (j + l) / 2];
        if (k < 9 && l < 9 && k > 0 && l > 0 && Fields[k, l] == null && Fields[i, j].color == pawnColor)
        {
            Fields[i, j] = null;
            Fields[k, l] = new Pawn(pawnSymbol, coordinatesB);

            if (inBetween != null && inBetween.symbol != pawnSymbol && Math.Abs(i - k) == 2)
            {
                deletedFlag = true;
                inBetween = null;
            }

            if (CanJumpOver((k+1,l+1), (k -1, l -1), enemyPawnSymbol))
            {
                jumpedFlag = true;
            }
            else if (CanJumpOver((k + 1, l -1), (k -1, l + 1), enemyPawnSymbol))
            {
                jumpedFlag = true;
            }
            else if (CanJumpOver((k -1, l + 1), (k + 1, l - 1), enemyPawnSymbol))
            {
                jumpedFlag = true;
            }
            else if (CanJumpOver((k -1, l -1), (k + 1, l + 1), enemyPawnSymbol))
            {
                jumpedFlag = true;
            }
        }
        if (deletedFlag == true)
        {
            Fields[(i + k) / 2, (j + l) / 2] = new Pawn(enemyPawnSymbol, ((i + k) / 2, (j + l) / 2));
        }
        Fields[i, j] = new Pawn(pawnSymbol, coordinatesA);
        Fields[k, l] = null;
        return jumpedFlag;
    }

    public Board(int n)
    {
        this.n = n;
        this.Fields = GenerateBoard(n);
        this.emptyFields = GenerateEmptyBoard(n);
    }
}
