using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polish_draughts
{
    public class AutoPlay
    {
        public AutoPlay() { }


        public void Play(Display display, Input getInput) {
            Board board = new Board(10);

            display.PrintBoard(board);
            while (!Game.CheckForWinner(board))
            {
                Round(board, display, getInput);
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
                Thread.Sleep(1000);
                madeMove = TryToMakeMove(board, pawnColor);
            }
            display.PrintBoard(board);
            gameOver = Game.CheckForWinner(board);
            madeMove = false;
            while (madeMove == false && gameOver == false)
            {
                string pawnColor = "black";
                Console.WriteLine("Black player's turn.");
                Thread.Sleep(1000);
                madeMove = TryToMakeMove(board, pawnColor);
            }
            display.PrintBoard(board);
            gameOver = Game.CheckForWinner(board);
        }

        public bool TryToMakeMove(Board board, string pawnColor)
        {
            ((int i, int j), (int i, int j)) chosenCoordTuple = bestCoordinates(board, pawnColor);
            (int i, int j) coordinatesA = chosenCoordTuple.Item1;
            (int i, int j) coordinatesB = chosenCoordTuple.Item2;

            if (board.IsValidMove(coordinatesA, coordinatesB))
            {
                Console.WriteLine($"Moved {pawnColor} pawn from {board.ToString(coordinatesA)} to {board.ToString(coordinatesB)}");
                board.MovePawn(board.Fields[coordinatesA.i, coordinatesA.j], coordinatesB);
                Thread.Sleep(1000);
                return true;
            }
            Console.WriteLine("Invalid move. Make sure to move diagonally and the space is not occupied.");
            return false;


        }

        public ((int i, int j),(int i, int j)) bestCoordinates(Board board, string pawnColor)
        {

            Random rnd = new Random();
            List<(int i, int j)> suggestedPawns = board.SuggestedPawn(pawnColor);
            int chosenPawnIndex = rnd.Next(suggestedPawns.Count);
            (int i, int j) coordinatesArand = suggestedPawns[chosenPawnIndex];
            List<(int i, int j)> suggestedMovesRand = board.SuggestedMove(coordinatesArand);
            int chosenMoveIndex = rnd.Next(suggestedMovesRand.Count);
            (int i, int j) coordinatesBrand = suggestedMovesRand[chosenMoveIndex];
            ((int i, int j),(int i, int j)) randomCoordinateTuple = (coordinatesArand, coordinatesBrand);




            for (int i = 0; i < suggestedPawns.Count; i++)
            {
                (int i, int j) coordinatesA = suggestedPawns[i];

                List<(int i, int j)> suggestedMoves = board.SuggestedMove(coordinatesA);

                for (int j = 0; j < suggestedMoves.Count; j++)
                {
                    (int i, int j) coordinatesB = suggestedMoves[j];
                    if (board.IsValidMove(coordinatesA, coordinatesB) && board.CanJumpOver(coordinatesA, coordinatesB, pawnColor) && !board.WillBeJumped(coordinatesA, coordinatesB, pawnColor))
                    {
                        ((int i, int j), (int i, int j)) bestCoordinateTuple = (suggestedPawns[i], suggestedMoves[j]);
                        Console.WriteLine("Used best coordinates");
                        return bestCoordinateTuple;
                    }
                    else if (board.IsValidMove(coordinatesA, coordinatesB) && board.CanJumpOver(coordinatesA, coordinatesB, pawnColor))
                    {
                        ((int i, int j), (int i, int j)) bestCoordinateTuple = (suggestedPawns[i], suggestedMoves[j]);
                        Console.WriteLine("Used next best coordinates");
                        return bestCoordinateTuple;
                    }
                }
            }
            Console.WriteLine("Used random coordinates");
            return randomCoordinateTuple;

        }

    }
}
