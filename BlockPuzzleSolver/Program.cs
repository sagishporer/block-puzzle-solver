using System;
using System.IO;

namespace BlockPuzzleSolver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DateTime startTime = DateTime.Now;

            using (StreamReader sr = new StreamReader("input.txt"))
            {
                Model p = Model.Load(sr);
                Console.WriteLine("Board size: {0}x{1}, Piece count: {2}", p.Rows, p.Columns, p.Pieces.Length);

                Solver s = new Solver(p);
                int solutionFound = s.SolveByRowColumn();

                Console.WriteLine("Solutions found: {0}", solutionFound);
            }

            Console.WriteLine("Total run time: {0}", new TimeSpan(DateTime.Now.Ticks - startTime.Ticks));
        }
    }
}
