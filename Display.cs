using System;
using System.ComponentModel;

/// <summary>
/// Prints game menu; prints board during ship placement;
/// Prints gameplay; prints outcome when game is over;
/// Only class to use writeline
/// </summary>
public class Display
{

    public string ShipASCII = @"                      .                v    
          v          /|                     
                    / |           v         
             v     /__|__                   
                 \--------/                 
~~~~~~~~~~~~~~~~~~`~~~~~~'~~~~~~~~~~~~~~~~~~";

    public string MainMenu = @"⭐Welcome to Polish Draughts!⭐
You can enter Q or QUIT anytime you wish to quit.
Please select game mode:
1: Player vs Player
2: AI vs AI";

    public Display()
    {

    }

    public static BackgroundWorker worker = new BackgroundWorker();
    public void MenuAnimation()
    {
        worker.DoWork += AnimationHandler;
        worker.WorkerSupportsCancellation = true;
        worker.RunWorkerAsync();
        Thread.Sleep(4000);
        worker.CancelAsync();
        Console.CursorVisible = true;
    }

    static void AnimationHandler(object sender, DoWorkEventArgs e)
    {
        string uno = @"                      .                v    
          v          /|                     
                    / |           v         
             v     /__|__                   
                 \--------/                 
~~~~~~~~~~~~~~~~~~`~~~~~~'~~~~~~~~~~~~~~~~~~";
        string tres = @"         v           .                v     
                    /|           v          
                   / |                      
            v     /__|__                    
                \--------/                  
~~~~~~~~~~~~~~~~~`~~~~~~'~~~~~~~~~~~~~~~~~~~";
        string dos = @"                       .                    
            v         /|                v   
               v     / |           v        
                    /__|__                  
                  \--------/                
~~~~~~~~~~~~~~~~~~~`~~~~~~'~~~~~~~~~~~~~~~~~";
        int i = 0;
        while (true)
        {
            if (worker.CancellationPending) break;
            i++;
            Console.CursorVisible = false;

            switch (i % 4)
            {
                case 0: PrintPicture(uno); break;
                case 1: PrintPicture(dos); break;
                case 2: PrintPicture(uno); break;
                case 3: PrintPicture(tres); break;
            }
            //Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
            Console.SetCursorPosition(Console.CursorLeft - 44, Console.CursorTop - 5);
            Thread.Sleep(400);


        }
    }

    public static void PrintPicture(string picture)
    {
        foreach (char c in picture)
        {
            if (c == '~')
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("~");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (c == 'v')
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("v");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Write(c);
            }
        }
    }

    public void PrintMainMenu()
    {
        //MenuAnimation();
        //Console.ForegroundColor = ConsoleColor.DarkYellow;
        //Console.WriteLine(ShipASCII);
        //Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(MainMenu);
    }

    public void PrintMenu(string menu)
    {
        Console.WriteLine(menu);
    }

    public void PrintGameplay(Board board, string message)
    {
        Console.Clear();
        Console.WriteLine(message);
    }

    public void PrintGameOver(string gameover)
    {
        Console.Clear();
        PrintPicture(ShipASCII);
        Console.WriteLine("\n" + gameover);
    }
    public void PrintBoard(Board board)
    {
        //Console.Clear();
        string writtenBoard = "";
        string writtenBoardRows = "";
        string iLetters = "   ";
        string bottomSpacer = iLetters;
        for (int i = 0; i < board.Fields.GetLength(0); i++)
        {
            string writtenBoardRow = $" {Convert.ToChar(i + 65)} ";
            if (i < 9)
            {
                iLetters += (" " + (i + 1) + " ");
                bottomSpacer += "   ";
            }
            else
            {
                iLetters += ((i + 1) + " ");
                bottomSpacer += "   ";
            }


            for (int j = 0; j < board.Fields.GetLength(1); j++)
            {
                writtenBoardRow += "";

                if (board.Fields[i, j] != null)
                {
                    writtenBoardRow += board.Fields[i, j].symbol;
                }
                else
                {
                    writtenBoardRow += board.emptyFields[i, j];
                }
            }
            writtenBoardRow += "   \n";
            writtenBoardRows += writtenBoardRow;
        }
        iLetters += "   \n";
        writtenBoard += iLetters;
        writtenBoard += writtenBoardRows;
        bottomSpacer += "   \n";
        writtenBoard += bottomSpacer;

        foreach (char c in writtenBoard)
        {
            if (c == 'w')
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("▓▓▓");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (c == 'b')
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("░░░");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (c == 'V')
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("███");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (c == 'W')
            {
                Console.Write("███");
            }
            else
            {

                Console.Write(c);
            }
        }

    }


}
