using System;
using System.Drawing;
using System.Text;

namespace Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Game myGame = new Game();
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}