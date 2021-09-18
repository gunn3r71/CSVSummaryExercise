using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.NetworkInformation;
using CSVSummaryExercise.Entities;
using CSVSummaryExercise.Extension;

namespace CSVSummaryExercise.Utils
{
    public class Csv
    {
        private const int ProductName = 0;
        private const int Price = 1;
        private const int Quantity = 2;

        public static void GenerateSummary(string path)
        {
            try
            {
                if (!File.Exists(path))
                    throw new IOException("File not found");

                ProcessingData(path);
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error ocurred: {e.Message}");
                throw;
            }
            catch (FormatException)
            {
                Console.WriteLine("Please, check data entered");
                throw;
            }
        }


        private static void ProcessingData(string path)
        {
            using (var reader = File.OpenText(path))
            {
                var orders = new List<Order>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(",");

                    orders.Add(ConvertToOrder(line));
                }

                var directoryPath = CreateDirectory(Path.GetDirectoryName(path), "out");

                var filePath = directoryPath + "summary.csv".ToDirectory();

                FillSummary(ref orders, filePath);
            }
        }

        private static void FillSummary(ref List<Order> orders, string filePath)
        {
            using (var writer = File.CreateText(filePath))
            {
                foreach (var order in orders)
                {
                    writer.WriteLine($"{order.ProductName}, {order.CalculateTotalAmount().ToString("F2", CultureInfo.InvariantCulture)}");
                }

                Console.WriteLine($"Operation completed, file created at {filePath}");
            }
        }

        private static string CreateDirectory(string path, string directoryName)
        {
            var newDirectory = path + $"{directoryName}".ToDirectory();

            Directory.CreateDirectory(newDirectory);

            return newDirectory;
        }

        private static Order ConvertToOrder(string[] line)
        {
            return new Order(line[ProductName], double.Parse(line[Price], CultureInfo.InvariantCulture), int.Parse(line[Quantity]));
        }
    }
}