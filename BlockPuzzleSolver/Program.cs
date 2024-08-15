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
                Console.Write("Board size: {0}x{1}, Piece count: {2}", p.Rows, p.Columns, p.Pieces.Length);
                Solver s = new Solver(p);
                s.Solve();
            }

            Console.WriteLine("Total run time: {0}", new TimeSpan(DateTime.Now.Ticks - startTime.Ticks));
        }

    }
}
