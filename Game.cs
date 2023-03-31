using polish_draughts;
using System;

public class Game
{
    public Game()
    {

        Start();
    }

    static int GetBoardSize(string input)
    {
        if (int.TryParse(input, out int n))
        {
            if (n < 10 || n > 20)
            {
                Console.WriteLine("The board size must be between 10 and 20!");
                return 0;
            }
            Console.WriteLine($"Board size set to {n}");
            return n;
        }
        else
        {
            Console.WriteLine("Please enter a number between 10 and 20 or type \"Q\" to quit");
            return 0;
        }
    }

    public void Start()
    {
        Display display = new Display();
        Input getInput = new Input();
        display.PrintMainMenu();
        int gameMode = 0;
        while (!(gameMode == 1 || gameMode == 2))
        {
            string input = getInput.GetUserInput();
            if (int.TryParse(input, out int modeNumber))
            {
                gameMode = modeNumber;
            }
        }
        if (gameMode == 2)
        {
            AutoPlay autoPlay = new AutoPlay();
            autoPlay.Play(display, getInput);
        }
        else
        {
            display.PrintMenu("Choose board size (10-20)");
            int n = 0;
            while (!(n >= 10 && n <= 20))
            {
                string input = getInput.GetUserInput();
                n = GetBoardSize(input);
            }

            Board board = new Board(n);

            display.PrintBoard(board);
            while (!CheckForWinner(board))
            {
                Round(board, display, getInput);
            }
        }
    }

    public void Round(Board board, Display display, Input getInput)
    {
        bool gameOver = false;
        bool madeMove = false;
        while (madeMove == false && gameOver == false)
        {
            string pawnColor = "white";
            Console.WriteLine("White player's turn.");
            madeMove = TryToMakeMove(board, pawnColor, getInput);
        }
        display.PrintBoard(board);
        gameOver = CheckForWinner(board);
        madeMove = false;
        while (madeMove == false && gameOver == false)
        {
            string pawnColor = "black";
            Console.WriteLine("Black player's turn.");
            madeMove = TryToMakeMove(board, pawnColor, getInput);
        }
        display.PrintBoard(board);
        gameOver = CheckForWinner(board);
    }

    public bool TryToMakeMove(Board board, string pawnColor, Input getInput)
    {
        Console.WriteLine($"Which pawn would you like to move?");
        List<(int i, int j)> suggestedPawns = board.SuggestedPawn(pawnColor);
        Console.Write("Suggestions:");
        foreach ((int i, int j) coord in suggestedPawns)
        { 
            Console.Write($" ({board.ToString(coord)})"); 
        }
        Console.Write("\n");
        (int i, int j) coordinatesA = getInput.GetCoordinates(board);
        if (board.Fields[coordinatesA.i, coordinatesA.j] == null || board.Fields[coordinatesA.i, coordinatesA.j].color != pawnColor)
        {
            Console.WriteLine($"Please select a {pawnColor} pawn");
            coordinatesA = getInput.GetCoordinates(board);
        }
        Console.WriteLine("Where would you like to move?");
        List<(int i, int j)> suggestedMoves = board.SuggestedMove(coordinatesA);
        Console.Write("Available moves:");
        foreach ((int i, int j) coord in suggestedMoves)
        {
            Console.Write($" ({board.ToString(coord)})");
        }
        Console.Write("\n");
        (int i, int j) coordinatesB = getInput.GetCoordinates(board);
        if (board.IsValidMove(coordinatesA, coordinatesB))
        {
            board.MovePawn(board.Fields[coordinatesA.i, coordinatesA.j], coordinatesB);
            return true;
        }
        Console.WriteLine("Invalid move. Make sure to move diagonally and the space is not occupied.");
        return false;
        
    }
	public static bool CheckForWinner(Board board)
	{
        //Checks whether there is a winner after each round; also checks for draws
        int whitePawns = 0;
        int blackPawns = 0;
        for (int i = 0; i < board.Fields.GetLength(0); i++)
        {
            for (int j = 0; j < board.Fields.GetLength(1); j++)
            {
                if (board.Fields[i,j] != null && board.Fields[i,j].symbol == board.whitePawn)
                {
                    whitePawns++;
                }
                else if (board.Fields[i,j] != null && board.Fields[i, j].symbol == board.blackPawn)
                            {
                    blackPawns++;
                }
            }
        }
        if (whitePawns == 0 || blackPawns == 0)
        {

            if (whitePawns > blackPawns)
            {
                Console.WriteLine("Congratulations! White player won.");
            }
            else if (whitePawns > blackPawns)
            {
                Console.WriteLine("Congratulations! Black player won.");
            }
            else
            {
                Console.WriteLine("Game is a draw; all pawns have been pwned.");
            }
            return true;

        }
        else
        {
            return false;
        }
	}


    
}
