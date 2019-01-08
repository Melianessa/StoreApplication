using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StoreApplication.Web
{
    public class StoreAPI
    {
        private string _apiBaseURI = "http://localhost:51743";

        public HttpClient InitializeClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_apiBaseURI);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
        
    }
    public class CategoryDTO
    {
        public Guid CategoryId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int CategorySortOrder { get; set; }
    }
    public class ProductDTO
    {
        public Guid ProductId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public CategoryDTO ProductCategory { get; set; }
        public string ProductImage { get; set; }
        public int ProductSortOrder { get; set; }

    }

    public class ProductDTOEdit
    {
        [Required(ErrorMessage = "Please select a role")]
        public Guid CategoryId { get; set; }
        public ProductDTO ProductDTO { get; set; }
        public List<CategoryDTO> CategoriesDTO { get; set; }
    }
}
