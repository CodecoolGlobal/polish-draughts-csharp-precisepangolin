using System;

public class Game
{
    public bool isWon;

    public Game()
    {
        isWon = false;
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
        while (isWon == false)
        {
            TryToMakeMove(board);
            board.WriteBoard();
            CheckForWinner(board);
        }

    }
	public void @TryToMakeMove(Board board)
	{
        //checks if the starting position from user input is a valid pawn and if the ending position
        //is within board boundaries.
        //If so, it calls @TryToMakeMove() on pawn instance.

        string input = "";
        while (input.Length < 2)
        {
            Console.WriteLine("Which pawn would you like to move?");
            input = @GetUserInput();
        }
        (int i, int j) coordinates = board.@ToCoordinates(input);
        int i = coordinates.i;
        int j = coordinates.j;
        if (board.Fields[i,j] != null)
        {
            Console.WriteLine("Where would you like to move?");
            input = @GetUserInput();
        }
        (int i, int j) second_coordinates = board.@ToCoordinates(input);
        board.@MovePawn(coordinates, second_coordinates);

        

    }
	public void @CheckForWinner(Board board)
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
            isWon = true;
            if (whitePawns > blackPawns) {
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

        }
	}
	public void Round()
	{
		Console.WriteLine("Checks which player is next and whether there is a winner");
	}

    public string @GetUserInput()
    {
        string input = Console.ReadLine();
        if (input.ToUpper() == "Q" || input.ToUpper() == "QUIT")
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
