using System;
using Northwind.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Northwind
{
    class Program
    {
        private static string _connectionString = @"server=(localdb)\MSSQLLocalDB;Database=Northwind";

        static void Main(string[] args)
        {
            //ToonCategories();
            //ToonSupppliers();
            //ToonProducts();
            int orderID = HaalOrderID();
            ToonOrder(orderID);
        }

        /*private static void ToonCategories()
        {
            var db = new NorthwindDbContext(_connectionString);
            var categories = db.Categories.ToList();
            foreach (var category in categories)
            {
                Console.Write($"{category.CategoryID}; ");
                Console.WriteLine(category.CategoryName);
            }
        } */

        private static void ToonProducts()
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine("Welke supplier wil je inzien?");
            var keuze = Console.ReadLine();
            var db = new NorthwindDbContext(_connectionString);
            var KeuzeS = Int32.Parse(keuze);
            var products = db.Products
                .Where(x => x.SupplierID == KeuzeS)
                .Include(b => b.Category)
                .Include(c => c.Supplier).ToList();
            foreach (var product in products)
            {
                Console.Write($"{product.ProductName}");
                Console.Write($",{product.Supplier.CompanyName}");
                Console.WriteLine($",{product.Category.CategoryName}");
            }
        }

        private static void ToonSupppliers()
        {
            var db = new NorthwindDbContext(_connectionString);
            var Suppliers = db.Suppliers.ToList();
            foreach (var supplier in Suppliers)
            {
                Console.Write($"{supplier.SupplierID}; ");
                Console.WriteLine($"{supplier.CompanyName}");
               
            }
        }

        private static int HaalOrderID()
        {
            int orderID = 0;
            bool trueorfalse = false;
            using (var db = new NorthwindDbContext(_connectionString))
            {
                while (trueorfalse == false)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(Environment.NewLine);
            
                    Console.WriteLine("Welke order wil je inzien?");
                    orderID = Int32.Parse(Console.ReadLine());
                    int KeuzeS = orderID;
                    
                    
                    var OrderIDLaag = db.Orders.OrderByDescending(b => b.OrderID).LastOrDefault();
                    var OrderIDHoog = db.Orders.OrderByDescending(b => b.OrderID).FirstOrDefault();
                    if (orderID >= OrderIDLaag.OrderID && orderID <= OrderIDHoog.OrderID)
                    {
                    trueorfalse = true;
                    }
                }               
            }
            return orderID;
        }

        private static void ToonOrder(int orderID)
        {
            Console.Clear();
            OrderHeader(orderID);
            OrderDetails(orderID);
        }

        private static void OrderHeader(int orderID)
        {
            using (var db = new NorthwindDbContext(_connectionString))
            {
                var Order = db.Orders
                .Where(b => b.OrderID == orderID).ToList();
                db.Categories.ToList();
                db.Customers.ToList();
                db.Products.ToList();
                foreach (var Orderr in Order)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{Orderr.Customer.CompanyName}");
                    Console.WriteLine($"{Orderr.Customer.ContactName}");
                    Console.WriteLine($"{Orderr.Customer.Address}");
                    Console.WriteLine($"{Orderr.Customer.City}");
                    Console.Write(Environment.NewLine);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($" Order: {orderID} ");
                    Console.WriteLine($"Date: {Convert.ToDateTime(Orderr.OrderDate).ToString("dd-M-yyyy")}");
                    Console.Write(Environment.NewLine);
                }
            }
        }

        private static void OrderDetails(int orderID)
        {
            using (var db = new NorthwindDbContext(_connectionString))
            {
                var OrderDetailss = db.OrderDetails
                .Where(b => b.OrderID == orderID)
                .Include(C => C.Product).ToList();


                db.Categories.ToList();
                db.Customers.ToList();
                db.Products.ToList();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($" {"Category", -25} {"Product", -35} {"UnitPrice", -10} {"Quantity", -10} {"Total", -10}");
                foreach (var Details in OrderDetailss)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine($" {Details.Product.Category.CategoryName, -25} {Details.Product.ProductName, -35} \u20AC {Math.Round(Details.UnitPrice,2), -10} {Details.Quantity, -10} \u20AC {Math.Round(Details.Quantity + Details.UnitPrice,2), -10}");
                }
            }
        }
    }
}
