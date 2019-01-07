using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApplication.Repository
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Category ProductCategory { get; set; }
        public string ProductImage { get; set; }
        public int ProductSortOrder { get; set; }
    }
}
