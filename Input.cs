using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polish_draughts
{
    public class Input
    {
        public Input() { }

        public string GetUserInput()
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

        public (int a, int b) GetCoordinates(Board board)
        {
            (int a, int b) coordinates;
            bool correctInput = false;
            string input = Console.ReadLine();
            while (!correctInput)
            {
                (int a, int b) coordinatesTest = board.ToCoordinates(input);
                int a = coordinatesTest.a;
                int b = coordinatesTest.b;
                if (input == null)
                {
                    Console.WriteLine("Input is null.");
                    input = Console.ReadLine();
                }
                else if (input.ToUpper() == "Q" || input.ToUpper() == "QUIT")
                {
                    Environment.Exit(0);
                }
                else if (a < 0 || b < 0 || a > board.Fields.GetLength(0) - 1 || b > board.Fields.GetLength(0) - 1)
                {
                    Console.WriteLine("Invalid input; please follow the format A1, B10 etc");
                    input = Console.ReadLine();
                }
                else
                {
                    correctInput= true;
                }
            }
            coordinates = board.ToCoordinates(input);
            return coordinates;
        }
    }
}
