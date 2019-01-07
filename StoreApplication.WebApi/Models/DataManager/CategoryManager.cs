using System;
using System.Collections.Generic;
using System.Linq;
using StoreApplication.Repository;

namespace StoreApplication.WebApi.Models.DataManager
{
    public class CategoryManager : IDataRepository<Category, Guid>
    {
        private ApplicationContext ctx;

        public CategoryManager(ApplicationContext c)
        {
            ctx = c;
        }
        public IEnumerable<Category> ViewAll()
        {
            var cat = ctx.Categories.ToList();
            return cat;
        }
        public Guid Create(Category b)
        {
            ctx.Categories.Add(b);
            b.CategoryId = Guid.NewGuid();
            b.CreationDate = DateTime.UtcNow;
            ctx.SaveChanges();
            return b.CategoryId;
        }

        public Guid Edit(Guid id, Category b)
        {
            var cat = ctx.Categories.Find(id);
            if (cat != null)
            {
                cat.CategoryDescription = b.CategoryDescription;
                cat.CategoryName = b.CategoryName;
                cat.CategorySortOrder = b.CategorySortOrder;
                ctx.SaveChanges();
            }
            return id;
        }

        public Category View(Guid id)
        {
            var cat = ctx.Categories.FirstOrDefault(p => p.CategoryId == id);
            return cat;
        }
        public Guid Delete(Guid id)
        {
            var cat = ctx.Categories.Find(id);
            if (cat != null)
            {
                ctx.Categories.Remove(cat);
                ctx.SaveChanges();
            }

            return id;
        }
    }
}
