using System;
using System.IO;
using System.Linq;

namespace Day_5a
{
    class Program
    {
        private const string InputPath = @"day5a-input.txt";

        static void Main(string[] args)
        {
            int[] code = File.ReadAllText(InputPath).Split(",").Select(int.Parse).ToArray();

            //code = new[] { 1002, 4, 3, 4, 33 };

            Intcode processor = new Intcode(code);
            processor.Process();
        }
    }
}
