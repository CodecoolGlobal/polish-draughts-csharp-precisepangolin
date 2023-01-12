using System;

public class Game
{
    public Game()
    {

        @Start();
    }

    static void WelcomeMessage()
    {
        Console.Write("⭐Welcome to Polish Draughts!⭐\nEnter \"Q\" or \"QUIT\" anytime you wish to quit.\n\nPlease enter board size (10-20):\n");
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
            Console.WriteLine("Please enter an integer or type \"Q\" to quit");
            return 0;
        }
    }

    public void @Start()
    {
        WelcomeMessage();
        int n = 0;
        while (!(n >= 10 && n <= 20))
        {
            string input = @GetUserInput();
            n = GetBoardSize(input);
        }

        Board board = new Board(n);

        board.WriteBoard();
        while (CheckForWinner(board) == false)
        {
            Round(board);

        }

    }
	public bool @TryToMakeMove(Board board, string pawnColor)
	{
        //checks if the starting position from user input is a valid pawn and if the ending position
        //is within board boundaries.
        //If so, it calls @TryToMakeMove() on pawn instance.

        string input = "";
        bool correctInput = false;
        (int i, int j) coordinates;
        int i;
        int j;
        while (input.Length < 2 || correctInput == false)
        {
            Console.WriteLine("Which pawn would you like to move?");
            input = @GetUserInput();
            (int a, int b) coordinatesTest = board.@ToCoordinates(input);
            int a = coordinatesTest.a;
            int b = coordinatesTest.b;
            if (input == null)
            {
                Console.WriteLine("Input is null.");
            }
            else if (a < 0 || b < 0 || a > board.Fields.GetLength(0)-1 || b > board.Fields.GetLength(0)-1)
            {
                Console.WriteLine("Invalid input; please follow the format A1, B10 etc");

            }
            else if (board.Fields[a, b] == null)
            {
                Console.WriteLine($"There are no pawns at {board.@ToString(coordinatesTest)}!");
            }
            else
            {
                correctInput = true;
            }
        }
        coordinates = board.@ToCoordinates(input);
        i = coordinates.i;
        j = coordinates.j;

        if (board.Fields[i,j] != null && board.Fields[i,j].color == pawnColor)
        {
            correctInput = false;

            while (input.Length < 2 || correctInput == false)
            {
                Console.WriteLine("Where would you like to move?");
                input = @GetUserInput();
                (int a, int b) coordinatesTest = board.@ToCoordinates(input);
                int a = coordinatesTest.a;
                int b = coordinatesTest.b;

                if (a < 0 || b < 0 || a > board.Fields.GetLength(0)-1 || b > board.Fields.GetLength(0)-1)
                {
                    Console.WriteLine("Invalid input; please follow the format A1, B10 etc");

                }
                else
                {
                    correctInput = true;
                }
            }
        }
        (int i, int j) second_coordinates = board.@ToCoordinates(input);

        
        return board.@MovePawn(coordinates, second_coordinates, pawnColor);



    }
	public bool @CheckForWinner(Board board)
	{
        //Checks whether there is a winner after each round; also checks for draws
        int whitePawns = 0;
        int blackPawns = 0;
        for (int i = 0; i < board.Fields.GetLength(0); i++)
        {
            for (int j = 0; j < board.Fields.GetLength(1); j++)
            {
                if (board.Fields[i,j] != null && board.Fields[i,j].color == board.whitePawn)
                {
                    whitePawns++;
                }
                else if (board.Fields[i,j] != null && board.Fields[i, j].color == board.blackPawn)
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
        else return false;
	}
	public void Round(Board board)
	{
        bool madeMove = false;
        while (madeMove == false)
        {
            string pawnColor = board.whitePawn;
            Console.WriteLine("White player's turn.");
            madeMove = TryToMakeMove(board, pawnColor);
        }
        board.WriteBoard();
        CheckForWinner(board);
        madeMove = false;
        while (madeMove == false)
        {
            string pawnColor = board.blackPawn;
            Console.WriteLine("Black player's turn.");
            madeMove = TryToMakeMove(board, pawnColor);
        }
        board.WriteBoard();
        CheckForWinner(board);
    }

    public string @GetUserInput()
    {
        string? input = Console.ReadLine();
        if (input == null)
        {
            Console.WriteLine("Input is null");
            return "?";
        }
        else if (input.ToUpper() == "Q" || input.ToUpper() == "QUIT")
        {
            Environment.Exit(0);
            return input;
        }
        else
        {
            return input;
        }
    }
}
