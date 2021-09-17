using System;
using CSVSummaryExercise.Utils;

namespace CSVSummaryExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Hi, Enter the file path: ");
            var path = Console.ReadLine();

            Csv.GenerateSummary(path);
        }
    }
}
