using System;
using System.Collections.Generic;
using System.Text;
using IceCreamSalesAssessment.Models;

namespace IceCreamSalesAssessment.Services
{
    public class ReportService
    {
        public List<SaleRecord> ReadSalesData(string filePath)
        {
            var sales = new List<SaleRecord>();

            var lines = File.ReadAllLines(filePath);

           
            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');

                
                if (parts.Length != 5)
                {
                    Console.WriteLine($"Invalid line skipped: {line}");
                    continue;
                }

                try
                {
                    var sale = new SaleRecord
                    {
                        Date = DateTime.Parse(parts[0].Trim()),
                        Sku = parts[1].Trim(),
                        UnitPrice = decimal.Parse(parts[2].Trim()),
                        Quantity = int.Parse(parts[3].Trim()),
                        TotalPrice = decimal.Parse(parts[4].Trim())
                    };

                    sales.Add(sale);
                }
                catch
                {
                    Console.WriteLine($"Invalid line skipped: {line}");
                }
            }

            return sales;
        }

   
        public decimal GetTotalSales(List<SaleRecord> sales)
        {
            return sales.Sum(x => x.TotalPrice);
        }

        public void GetMonthWiseSales(List<SaleRecord> sales)
        {
            Console.WriteLine("\nMONTH WISE SALES");

            var result = sales
                .GroupBy(x => x.Date.ToString("yyyy-MM"))
                .Select(g => new
                {
                    Month = g.Key,
                    Total = g.Sum(x => x.TotalPrice)
                })
                .OrderBy(x => x.Month);

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Month} : {item.Total}");
            }
        }

        
        public void GetMostPopularItemEachMonth(List<SaleRecord> sales)
        {
            Console.WriteLine("\nMOST POPULAR ITEM EACH MONTH");

            var monthlyGroups = sales
                .GroupBy(x => x.Date.ToString("yyyy-MM"));

            foreach (var month in monthlyGroups)
            {
                var popularItem = month
                    .GroupBy(x => x.Sku)
                    .Select(g => new
                    {
                        Item = g.Key,
                        Quantity = g.Sum(x => x.Quantity)
                    })
                    .OrderByDescending(x => x.Quantity)
                    .First();

                Console.WriteLine(
                    $"{month.Key} : {popularItem.Item} ({popularItem.Quantity})");
            }
        }

        public void GetHighestRevenueItemEachMonth(List<SaleRecord> sales)
        {
            Console.WriteLine("\nHIGHEST REVENUE ITEM EACH MONTH");

            var monthlyGroups = sales
                .GroupBy(x => x.Date.ToString("yyyy-MM"));

            foreach (var month in monthlyGroups)
            {
                var highestRevenue = month
                    .GroupBy(x => x.Sku)
                    .Select(g => new
                    {
                        Item = g.Key,
                        Revenue = g.Sum(x => x.TotalPrice)
                    })
                    .OrderByDescending(x => x.Revenue)
                    .First();

                Console.WriteLine(
                    $"{month.Key} : {highestRevenue.Item} ({highestRevenue.Revenue})");
            }
        }

        public void GetNonMovingItems(List<SaleRecord> sales)
        {
            Console.WriteLine("\nNON MOVING ITEMS");

            var groupedItems = sales.GroupBy(x => x.Sku);

            foreach (var item in groupedItems)
            {
                var months = item
                    .Select(x => x.Date.ToString("yyyy-MM"))
                    .Distinct()
                    .Count();

                if (months == 1)
                {
                    Console.WriteLine(item.Key);
                }
            }
        }

      
        public void GetInvalidRecords(List<SaleRecord> sales)
        {
            Console.WriteLine("\nINVALID RECORDS");

            var invalidRecords = sales.Where(x =>
                x.Quantity <= 0 ||
                x.UnitPrice <= 0 ||
                (x.UnitPrice * x.Quantity) != x.TotalPrice);

            foreach (var item in invalidRecords)
            {
                Console.WriteLine(
                    $"{item.Date:yyyy-MM-dd} | " +
                    $"{item.Sku} | " +
                    $"{item.UnitPrice} | " +
                    $"{item.Quantity} | " +
                    $"{item.TotalPrice}");
            }
        }
    }
}


 