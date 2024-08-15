using System;

namespace BlockPuzzleSolver
{
    class Solver
    {
        private const string PIECE_SYMBOL = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        private Model Problem { get; set; }

        private bool PrintSolutions { get; set; }

        private long StartTimeTicks { get; set; }

        public Solver(Model problem, bool printSolutions)
        {
            if (problem.Pieces.Length > PIECE_SYMBOL.Length)
                throw new Exception("Too many pieces. Not enough solution symbols. Max is: " + PIECE_SYMBOL.Length);

            this.Problem = problem;
            this.PrintSolutions = printSolutions;
            this.StartTimeTicks = DateTime.Now.Ticks;
        }

        /// <summary>
        /// Idea: fill the puzzle row by row.
        /// 
        /// Algorithm:
        /// 1. If board completed - print solution, and go back.
        /// 2. Select next empty position (filling rows first).
        /// 3. Iterate the all unused pieces, try to place them. If placing is possible - go to step 1.
        /// </summary>
        /// <returns>Number of solutions found</returns>
        public int SolveByRowColumn()
        {
            Board board = new Board(this.Problem.Rows, this.Problem.Columns);
            bool[] usedPieces = new bool[this.Problem.Pieces.Length];
            for (int i = 0; i < usedPieces.Length; i++)
                usedPieces[i] = false;

            return SolveByRowColumn(0, 0, board, usedPieces);
        }

        private int SolveByRowColumn(int nextRow, int nextColumn, Board board, bool[] usedPieces)
        {
            if (nextColumn >= board.Columns)
            {
                nextColumn = 0;
                nextRow++;
            }

            // Reached the end of the board
            if (nextRow >= board.Rows)
            {
                // Check that all the pieces were used
                foreach (bool usedPiece in usedPieces)
                    if (!usedPiece)
                        return 0;

                // Found a solution
                if (this.PrintSolutions)
                {
                    Console.WriteLine("Run time: {0}", new TimeSpan(DateTime.Now.Ticks - this.StartTimeTicks));
                    Console.WriteLine(board);
                }

                return 1;
            }

            int solutionsFound = 0;
            // If position is not empty - recuse
            if (!board.IsEmpty(nextRow, nextColumn))
            {
                solutionsFound += SolveByRowColumn(nextRow, nextColumn + 1, board, usedPieces);
            }
            else
            {
                // Try to place pieces & recuse
                for (int i = 0; i < usedPieces.Length; i++)
                {
                    if (usedPieces[i] == true)
                        continue;

                    Piece piece = this.Problem.Pieces[i];
                    foreach (PieceRotation rotation in piece.Rotations)
                    {
                        int placingColumn = nextColumn - rotation.FirstBlockColumnInRowZero;
                        if ((placingColumn < 0)||(placingColumn + rotation.Columns > board.Columns))
                            continue;
                        if (nextRow + rotation.Rows > board.Rows)
                            continue;

                        if (!board.CanPlaceHere(nextRow, placingColumn, rotation))
                            continue;

                        usedPieces[i] = true;
                        board.PlaceHere(nextRow, placingColumn, rotation, PIECE_SYMBOL[i]);

                        solutionsFound += SolveByRowColumn(nextRow, nextColumn + 1, board, usedPieces);

                        board.PlaceHere(nextRow, placingColumn, rotation, ' ');
                        usedPieces[i] = false;
                    }
                }
            }

            return solutionsFound;
        }

        /// <summary>
        /// Idea: try to place pieces and see if you can place all the pieces.
        /// 
        /// Algorithm:
        /// 1. If board completed - print solution, and go back.
        /// 2. Select next piece.
        /// 3. Iterate the entire board, try to place it. If placing is possible - go to step 1.
        /// </summary>
        /// <returns>Number of solutions found</returns>
        public int SolveByPieces()
        {
            Board board = new Board(this.Problem.Rows, this.Problem.Columns);

            return SolveByPieces(0, board);
        }

        private int SolveByPieces(int pieceNum, Board board)
        {
            // Solution found
            if (pieceNum >= this.Problem.Pieces.Length)
            {
                if (this.PrintSolutions)
                {
                    Console.WriteLine("Run time: {0}", new TimeSpan(DateTime.Now.Ticks - this.StartTimeTicks));
                    Console.WriteLine(board);
                }

                return 1;
            }

            int solutionsFound = 0;
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
                        solutionsFound += SolveByPieces(pieceNum + 1, board);

                        // Remove the piece
                        board.PlaceHere(row, column, rotation, ' ');
                    }
            }

            return solutionsFound;
        }
    }
}
