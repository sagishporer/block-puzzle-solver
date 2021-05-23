using System;
using System.Text;

namespace BlockPuzzleSolver
{
    class PieceRotation : IEquatable<PieceRotation>
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public Point[] Blocks { get; private set; }

        private int HashCode { get; set; }

        public PieceRotation(Point[] blocks)
        {
            int maxRow = 0;
            int maxColumn = 0;
            foreach (Point block in blocks)
            {
                maxColumn = Math.Max(maxColumn, block.Column);
                maxRow = Math.Max(maxRow, block.Row);
            }

            Rows = maxRow + 1;
            Columns = maxColumn + 1;
            Blocks = blocks;
            Array.Sort(this.Blocks);

            HashCode = 0;
            foreach (Point block in blocks)
                HashCode += block.Row + block.Column;
        }

        public PieceRotation MirrorHorizontal()
        {
            Point[] newPositions = new Point[this.Blocks.Length];
            for (int i = 0; i < this.Blocks.Length; i++)
            {
                Point cellLocation = this.Blocks[i];
                newPositions[i] = new Point(cellLocation.Row, Columns - cellLocation.Column - 1);
            }

            return new PieceRotation(newPositions);
        }

        public PieceRotation RotateClockWise()
        {
            Point[] newPositions = new Point[this.Blocks.Length];
            for (int i = 0; i < this.Blocks.Length; i++)
            {
                Point cellLocation = this.Blocks[i];
                newPositions[i] = new Point(cellLocation.Column, Rows - cellLocation.Row - 1);
            }

            return new PieceRotation(newPositions);
        }

        public override int GetHashCode()
        {
            return this.HashCode;
        }

        public override bool Equals(object obj)
        {
            return obj is Point && Equals((Point)obj);
        }

        public bool Equals(PieceRotation other)
        {
            if (other == null)
                return false;

            if (other.Blocks.Length != this.Blocks.Length)
                return false;

            for (int i = 0; i < this.Blocks.Length; i++)
            {
                if (!this.Blocks[i].Equals(other.Blocks[i]))
                    return false;
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            bool[,] array = new bool[Rows,Columns];
            for (int row = 0; row < Rows; row++)
                for (int col = 0; col < Columns; col++)
                    array[row,col] = false;

            foreach (Point cellLocation in this.Blocks)
                array[cellLocation.Row,cellLocation.Column] = true;

            for (int row = 0; row < this.Rows; row++)
            {
                for (int col = 0; col < this.Columns; col++)
                    if (array[row,col])
                        sb.Append('*');
                    else
                        sb.Append(' ');

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }

    public class Point : IEquatable<Point>, IComparable<Point>
    {
        public int Column { get; private set; }
        public int Row { get; private set; }

        public Point(int row, int column)
        {
            Column = column;
            Row = row;
        }

        public bool Equals(Point other)
        {
            if (other == null)
                return false;

            return (this.Column == other.Column) && (this.Row == other.Row);
        }

        public int CompareTo(Point other)
        {
            return (this.Column * this.Column + this.Row * this.Row) - (other.Column * other.Column + other.Row * other.Row);
        }
    }
}
