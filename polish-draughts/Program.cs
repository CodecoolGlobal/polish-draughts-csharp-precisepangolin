using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace Chess
{
    public class Pawn
    {
        public string color;
        public (int x, int y) coordinates;
        public bool IsCrowned;

        public Pawn(string color, (int x, int y) coordinates)
        {
            this.color = color;
            this.coordinates = coordinates;
        }

    }

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

        public Board(int n)
        {
            this.n = n;
            this.Fields = GenerateBoard(n);
        }
    }


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
            Console.ReadLine();
        }
    }
}