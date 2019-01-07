using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreApplication.Repository;

namespace StoreApplication.WebApi.Models.DataManager
{
    public class ProductManager : IDataRepository<Product, Guid>
    {
        private ApplicationContext ctx;

        public ProductManager(ApplicationContext c)
        {
            ctx = c;
        }
        public IEnumerable<Product> ViewAll()
        {
            var product = ctx.Products.Include(p=> p.ProductCategory).ToList();
            return product;
        }
        public Guid Create(Product b)
        {
            ctx.Products.Add(b);
            b.ProductId = new Guid();
            ctx.SaveChanges();
            return b.ProductId;
        }

        public Guid Edit(Guid id, Product b)
        {
            var product = ctx.Products.Find(id);
            if (product != null)
            {
                product.ProductCategory = b.ProductCategory;
                product.ProductDescription = b.ProductDescription;
                product.ProductImage = b.ProductImage;
                product.ProductName = b.ProductName;
                product.ProductSortOrder = b.ProductSortOrder;
                ctx.SaveChanges();
            }
            return id;
        }

        public Product View(Guid id)
        {
            var product = ctx.Products.FirstOrDefault(p => p.ProductId == id);
            return product;
        }

        public Guid Delete(Guid id)
        {
            var product = ctx.Products.Find(id);
            if (product!=null)
            {
                ctx.Products.Remove(product);
                ctx.SaveChanges();
            }

            return id;
        }
    }
}
