using System;

public class Game
{
	public void @Start()
	{
		Console.WriteLine("Start game between players");
	}
	public void @TryToMakeMove()
	{
		Console.WriteLine("checks if the starting position from user input is a valid pawn and if the ending position is within board boundaries. " +
			"If so, it calls @TryToMakeMove() on pawn instance");
	}
	public void @CheckForWinner()
	{
		Console.WriteLine("Checks whether there is a winner after each round; also checks for draws");
	}
	public void Round()
	{
		Console.WriteLine("Checks which player is next and whether there is a winner");
	}
	public Game()
	{
		Console.WriteLine("Game");
	}
}
