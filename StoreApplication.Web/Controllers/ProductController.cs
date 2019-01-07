using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreApplication.Web;

namespace StoreApplication.Web.Controllers
{
    public class ProductController : Controller
    {
        StoreAPI _productAPI = new StoreAPI();
        public async Task<IActionResult> Index()
        {
            List<ProductDTO> dto = new List<ProductDTO>();
            HttpClient client = _productAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync("api/product");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<ProductDTO>>(result);
            }
            return View(dto);
        }

        public async Task<IActionResult> Create()
        {
            ProductDTOEdit pdto = new ProductDTOEdit();
            HttpClient client = _productAPI.InitializeClient();
            HttpResponseMessage resCategory = await client.GetAsync("api/category");
            if (resCategory.IsSuccessStatusCode)
            {
                var resultCategory = await resCategory.Content.ReadAsStringAsync();
                pdto.CategoriesDTO = JsonConvert.DeserializeObject<List<CategoryDTO>>(resultCategory);
            }
            return View(pdto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind(
                "ProductId,CreationDate,ProductName,ProductDescription,ProductCategory,ProductImage,ProductSortOrder")]
            ProductDTOEdit product)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _productAPI.InitializeClient();
                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PostAsync("api/product", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductDTOEdit pdto = new ProductDTOEdit();
            HttpClient client = _productAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync($"api/product/{id}");
            HttpResponseMessage resCategory = await client.GetAsync("api/category");
            if (res.IsSuccessStatusCode & resCategory.IsSuccessStatusCode)
            {
                pdto.ProductDTO = JsonConvert.DeserializeObject<ProductDTO>(await res.Content.ReadAsStringAsync());
                pdto.CategoriesDTO = JsonConvert.DeserializeObject<List<CategoryDTO>>(await resCategory.Content.ReadAsStringAsync());
            }

            return View(pdto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id,
            [Bind(
                "ProductId,CreationDate,ProductName,ProductDescription,ProductCategory,ProductImage,ProductSortOrder")]
            ProductDTO product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                HttpClient client = _productAPI.InitializeClient();
                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PutAsync("api/product", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(product);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<ProductDTO> dto = new List<ProductDTO>();
            HttpClient client = _productAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync("api/product");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<ProductDTO>>(result);
            }

            var product = dto.SingleOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            HttpClient client = _productAPI.InitializeClient();
            HttpResponseMessage res = client.DeleteAsync($"api/product/{id}").Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}