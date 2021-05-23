using System;
using System.IO;

namespace BlockPuzzleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startTime = DateTime.Now;

            using (StreamReader sr = new StreamReader("input.txt"))
            {
                Model p = Model.Load(sr);
                Solver s = new Solver(p);
                s.Solve();
            }

            Console.WriteLine("Total run time: {0}", new TimeSpan(DateTime.Now.Ticks - startTime.Ticks));
        }

    }
}
