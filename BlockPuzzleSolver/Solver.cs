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
            char[,] board = new char[this.Problem.Rows, this.Problem.Columns];
            for (int row = 0; row < this.Problem.Rows; row++)
                for (int col = 0; col < this.Problem.Columns; col++)
                    board[row, col] = ' ';

            Solve(0, board);
        }

        private bool Solve(int pieceNum, char[,] board)
        {
            // Solution found, print it
            if (pieceNum >= this.Problem.Pieces.Length)
            {
                Console.WriteLine("Run time: {0}", new TimeSpan(DateTime.Now.Ticks - this.StartTimeTicks));
                PrintBoard(board);
                return true;
            }

            Piece piece = this.Problem.Pieces[pieceNum];
            foreach (PieceRotation rotation in piece.Rotations)
            {
                for (int row = 0; row < this.Problem.Rows - rotation.Rows + 1; row++)
                    for (int column = 0; column < this.Problem.Columns - rotation.Columns + 1; column++)
                    {
                        // Try placing the piece
                        if (!CanPlaceHere(board, row, column, rotation))
                            continue;                        

                        PlaceHere(board, row, column, rotation, PIECE_SYMBOL[pieceNum]);

                        // Recurse - don't stop when a solution found
                        Solve(pieceNum + 1, board);

                        // Recurse - stop when a solution found
                        //if (Solve(pieceNum + 1, board))
                        //    return true;

                        // Remove the piece
                        PlaceHere(board, row, column, rotation, ' ');
                    }
            }

            return false;
        }

        private static bool CanPlaceHere(char[,] board, int row, int column, PieceRotation rotation)
        {
            foreach (Point p in rotation.Blocks)
                if (board[row + p.Row, column + p.Column] != ' ')
                    return false;

            return true;
        }

        private static void PlaceHere(char[,] board, int row, int column, PieceRotation rotation, char ch)
        {
            foreach (Point p in rotation.Blocks)
                board[row + p.Row, column + p.Column] = ch;
        }

        private void PrintBoard(char[,] board)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-----------------------------");

            for (int row = 0; row < this.Problem.Rows; row++)
            {
                for (int col = 0; col < this.Problem.Columns; col++)
                    sb.Append(board[row, col]);
                sb.AppendLine();
            }
            sb.AppendLine("-----------------------------");

            Console.Write(sb);
        }
    }
}
