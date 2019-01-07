using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplication.Repository
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CategoryId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int CategorySortOrder { get; set; }
    }
}
