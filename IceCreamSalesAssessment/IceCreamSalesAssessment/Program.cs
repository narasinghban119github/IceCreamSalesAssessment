using IceCreamSalesAssessment.Services;

namespace IceCreamSalesAssessment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ICE CREAM SALES REPORT");
            Console.WriteLine("--------------------------");

            string filePath = @"C:\Users\Lenovo\source\repos\IceCreamSalesAssessment\IceCreamSalesAssessment\Data\sales.txt";

            var service = new ReportService();

            var sales = service.ReadSalesData(filePath);

            Console.WriteLine($"\nTotal Records: {sales.Count}");

            Console.WriteLine($"\nTOTAL SALES: {service.GetTotalSales(sales)}");

            service.GetMonthWiseSales(sales);

            service.GetMostPopularItemEachMonth(sales);

            service.GetHighestRevenueItemEachMonth(sales);

            service.GetInvalidRecords(sales);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}