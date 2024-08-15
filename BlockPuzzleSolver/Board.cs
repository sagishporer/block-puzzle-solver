using System;
using System.Text;

namespace BlockPuzzleSolver
{
	public class Board
	{
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        private char[,] BoardData;

        public Board(int rows, int columns)
		{
            this.Rows = rows;
            this.Columns = columns;

            BoardData = new char[this.Rows, this.Columns];
            for (int row = 0; row < this.Rows; row++)
                for (int col = 0; col < this.Columns; col++)
                    BoardData[row, col] = ' ';
        }

        public bool IsEmpty(int row, int column)
        {
            return BoardData[row, column] == ' ';
        }

        public bool CanPlaceHere(int row, int column, PieceRotation rotation)
        {
            foreach (Point p in rotation.Blocks)
                if (BoardData[row + p.Row, column + p.Column] != ' ')
                    return false;

            return true;
        }

        public void PlaceHere(int row, int column, PieceRotation rotation, char ch)
        {
            foreach (Point p in rotation.Blocks)
                BoardData[row + p.Row, column + p.Column] = ch;
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-----------------------------");

            for (int row = 0; row < this.Rows; row++)
            {
                for (int col = 0; col < this.Columns; col++)
                    sb.Append(BoardData[row, col]);
                sb.AppendLine();
            }
            sb.AppendLine("-----------------------------");

            return sb.ToString();
        }
    }
}

