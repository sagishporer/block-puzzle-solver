using System;
using System.Text;

namespace BlockPuzzleSolver
{
    class Solver
    {
        private const string PIECE_SYMBOL = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private Model Problem { get; set; }

        private long StartTimeTicks { get; set; }

        public Solver(Model problem)
        {
            this.Problem = problem;
            this.StartTimeTicks = DateTime.Now.Ticks;
        }

        public void Solve()
        {
            Board board = new Board(this.Problem.Rows, this.Problem.Columns);

            Solve(0, board);
        }

        private bool Solve(int pieceNum, Board board)
        {
            // Solution found, print it
            if (pieceNum >= this.Problem.Pieces.Length)
            {
                Console.WriteLine("Run time: {0}", new TimeSpan(DateTime.Now.Ticks - this.StartTimeTicks));
                Console.WriteLine(board);

                return true;
            }

            Piece piece = this.Problem.Pieces[pieceNum];
            foreach (PieceRotation rotation in piece.Rotations)
            {
                for (int row = 0; row < this.Problem.Rows - rotation.Rows + 1; row++)
                    for (int column = 0; column < this.Problem.Columns - rotation.Columns + 1; column++)
                    {
                        // Try placing the piece
                        if (!board.CanPlaceHere(row, column, rotation))
                            continue;                        

                        board.PlaceHere(row, column, rotation, PIECE_SYMBOL[pieceNum]);

                        // Recurse - don't stop when a solution found
                        Solve(pieceNum + 1, board);

                        // Recurse - stop when a solution found
                        //if (Solve(pieceNum + 1, board))
                        //    return true;

                        // Remove the piece
                        board.PlaceHere(row, column, rotation, ' ');
                    }
            }

            return false;
        }
    }
}
