using System;
using System.Drawing;

namespace Chess
{
    internal class Program
    {
        static void WelcomeMessage()
        {
            Console.Write("Welcome to Polish Draughts!\nPlease enter board size (10-20):\n");
        }

        static int GetN(string input)
        {
            
            if (int.TryParse(input, out int n))
            {
                if (n < 10 || n > 20)
                {
                    Console.WriteLine("The board size must be between 10 and 20!");
                    return n;
                }
                Console.WriteLine($"Board size set to {n}");
                return n;
            }
            else
            {
                Console.WriteLine("Please enter an integer!");
                return n;
            } 
        }

        static void Main(string[] args)
        {
            WelcomeMessage();
            int n = 0;
            while (!(n >= 10 && n <= 20))
            {
                string input = Console.ReadLine();
                n = GetN(input); 
            }

            Board board = new Board(n);
        
            board.WriteBoard();
            Console.WriteLine(board.@ToString());
            board.@RemovePawn((0, 1));
            board.@MovePawn((1, 1), (2, 2));
            Console.ReadLine();
        }
    }
}