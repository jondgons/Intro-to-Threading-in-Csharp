/* Name: Cody Gonsowski
 * Date: 10/10/2019
 * File: Program.cs
 * Desc: Estimates the value of pi using the Monte Carlo method with a user-inputted number of darts and threads.
 * XC:   Stopwatch
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro_to_Threading_in_Csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<System.Threading.Thread> threads = new List<System.Threading.Thread>();
            List<FindPiThread> piThreads = new List<FindPiThread>();
            double totalDartsIn = 0;
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

            Console.Write("How many darts do you want thrown? ");
            int toThrow = Convert.ToInt32(Console.ReadLine());

            Console.Write("How many threads do you wish to enslave? ");
            int threadCount = Convert.ToInt32(Console.ReadLine());

            timer.Start();

            for (int i = 0; i < threadCount; i++)
            {
                piThreads.Add(new FindPiThread(toThrow));
                threads.Add(new System.Threading.Thread(new System.Threading.ThreadStart(piThreads[i].throwDarts)));
                threads[i].Start();
                System.Threading.Thread.Sleep(16);
            }

            for (int i = 0; i < threadCount; i++)
            {
                threads[i].Join();
            }

            for (int i = 0; i < threadCount; i++)
            {
                totalDartsIn += piThreads[i].dartLand;
            }

            Console.WriteLine("Pi Evaluation: " + (4 * totalDartsIn / (threadCount * toThrow)));

            timer.Stop();

            Console.WriteLine("Time Taken: " + timer.ElapsedMilliseconds + "ms");

            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
        }
    }


    class FindPiThread
    {
        private int dartThrows;
        public int dartLand { get; set; }
        private Random placer;

        public FindPiThread(int throws)
        {
            dartThrows = throws;
            dartLand = 0;
            placer = new Random();
        }

        public void throwDarts()
        {
            for (int i = 0; i < dartThrows; i++)
            {
                double hypo = Math.Sqrt(Math.Pow(placer.NextDouble(), 2.0) + Math.Pow(placer.NextDouble(), 2.0));

                if (hypo <= 1.0)
                {
                    dartLand += 1;
                }
            }
        }
    }
}
