using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CSVSummaryExercise.Entities;

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

                using (var reader = File.OpenText(path))
                {
                    var orders = new List<Order>();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine().Split(",");
                        var order = new Order(line[ProductName], double.Parse(line[Price], CultureInfo.InvariantCulture), int.Parse(line[Quantity]));
                        
                        orders.Add(order);
                    }

                    var newDirectoryPath = Path.GetDirectoryName(path) + @"\out\";

                    Directory.CreateDirectory(newDirectoryPath);

                    var filePath = newDirectoryPath + "summary.csv";

                    using (var writer = File.CreateText(filePath))
                    {
                        foreach (var order in orders)
                        {
                            var total = order.Price * order.Quantity;
                            writer.WriteLine($"{order.ProductName}, {total.ToString("F2", CultureInfo.InvariantCulture)}");
                        }

                        Console.WriteLine($"Operation completed, file created in {filePath}");
                    }
                }
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
    }
}