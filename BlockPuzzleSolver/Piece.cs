using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlockPuzzleSolver
{
    class Piece
    {
        public PieceRotation[] Rotations { get; private set; }

        public Piece(Point[] blocks)
        {
            HashSet<PieceRotation> piecesHash = new HashSet<PieceRotation>();
            PieceRotation rotation = new PieceRotation(blocks);
            piecesHash.Add(rotation);
            for (int r = 0; r < 3; r++)
            {
                rotation = rotation.RotateClockWise();
                piecesHash.Add(rotation);
            }

            rotation = rotation.MirrorHorizontal();
            piecesHash.Add(rotation);
            for (int r = 0; r < 3; r++)
            {
                rotation = rotation.RotateClockWise();
                piecesHash.Add(rotation);
            }

            this.Rotations = piecesHash.ToArray();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (PieceRotation rotation in this.Rotations)
            {
                sb.Append(rotation.ToString());
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
