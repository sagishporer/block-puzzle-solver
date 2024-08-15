using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlockPuzzleSolver
{
    class Model
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public Piece[] Pieces { get; private set; }

        public Model(int rows, int columns, Piece[] pieces)
        {
            this.Rows = rows;
            this.Columns = columns;
            this.Pieces = pieces;

            // Validate model
            if (this.Rows == 0)
                throw new Exception("Rows == 0");
            if (this.Columns == 0)
                throw new Exception("Columns == 0");
            if (this.Pieces == null)
                throw new Exception("Pieces == null");
            if (this.Pieces.Length == 0)
                throw new Exception("Pieces.Length == 0");

            int blocksCount = 0;
            foreach (Piece piece in this.Pieces)
            {
                if (piece.Rotations.Length == 0)
                    throw new Exception("Piece without rotations");
                blocksCount += piece.Rotations[0].Blocks.Length;
            }

            if (blocksCount != this.Rows * this.Columns)
                throw new Exception(string.Format("Invalid blocks count, board size: {0}, sum of blocks: {1}",
                    this.Rows * this.Columns, blocksCount));
        }

        public static Model Load(StreamReader sr)
        {
            string line;
            string[] parts;
            line = sr.ReadLine();
            parts = line.Split(" ");

            int rows = int.Parse(parts[0]);
            int columns = int.Parse(parts[1]);
            List<Piece> pieces = new List<Piece>();
            List<Point> blocks = new List<Point>();
            int pieceRow = 0;
            while ((line = sr.ReadLine()) != null)
            {
                line = line.TrimEnd();
                if (line.Length == 0)
                {
                    pieces.Add(new Piece(blocks.ToArray()));
                    blocks.Clear();
                    pieceRow = 0;
                    continue;
                }

                for (int pieceColumn = 0; pieceColumn < line.Length; pieceColumn++)
                    if (line[pieceColumn] != ' ')
                        blocks.Add(new Point(pieceRow, pieceColumn));

                pieceRow++;
            }

            // Add last piece
            if (blocks.Count > 0)
                pieces.Add(new Piece(blocks.ToArray()));

            return new Model(rows, columns, pieces.ToArray());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Piece piece in this.Pieces)
            {
                sb.Append(piece);
                sb.Append("-------------------------------------");
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
