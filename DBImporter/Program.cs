using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreApplication.Repository;

namespace DBImporter
{
    public class DbImport
    {
        public static void Main(string[] args)
        {
            using (var db = new ApplicationContext())
            {
                //var categories = new List<Category>
                //{
                //    new Category()
                //    {
                //        CategoryId = Guid.NewGuid(),
                //        CategorySortOrder = 0,
                //        CategoryName = "Bakery",
                //        CategoryDescription = "Bakery products",
                //        CreationDate = DateTime.UtcNow
                //    },
                //    new Category()
                //    {
                //        CategoryId = Guid.NewGuid(),
                //        CategorySortOrder = 1,
                //        CategoryName = "Milk",
                //        CategoryDescription = "Milk products",
                //        CreationDate = DateTime.UtcNow
                //    }
                //};

                //db.Categories.AddRange(categories);

                //var products = new List<Product>
                //{
                //    new Product()
                //    {
                //        ProductId = Guid.NewGuid(),
                //        ProductSortOrder = 0,
                //        ProductName = "cake",
                //        ProductDescription = "lemon cake with white chocolate",
                //        CreationDate = DateTime.UtcNow,
                //        ProductCategory = db.Categories.FirstOrDefault(p => p.CategoryName=="Bakery")
                //    },
                //    new Product()
                //    {
                //        ProductId = Guid.NewGuid(),
                //        ProductSortOrder = 0,
                //        ProductName = "yogurt",
                //        ProductDescription = "strawberry yougurt with pineapple",
                //        CreationDate = DateTime.UtcNow,
                //        ProductCategory = db.Categories.FirstOrDefault(p => p.CategoryName=="Milk")
                //    },
                //};

                //db.Products.AddRange(products);
                db.SaveChanges();
            }

            Console.WriteLine("ok");
        }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=TestDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
