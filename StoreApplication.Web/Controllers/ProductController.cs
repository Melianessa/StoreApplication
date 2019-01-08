using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreApplication.Web;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Mime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;

namespace StoreApplication.Web.Controllers
{
    public class ProductController : Controller
    {
        StoreAPI _productAPI = new StoreAPI();
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
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
        public async Task<IActionResult> Create([Bind("CategoryId,CategoriesDTO,ProductDTO")] ProductDTOEdit product)
        {
            HttpClient client = _productAPI.InitializeClient();
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                        if (file.Length > 0)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                            Console.WriteLine(fileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                product.ProductDTO.ProductImage = file.FileName;
                            }
                        }
                    }
                }
                product.ProductDTO.ProductCategory = new CategoryDTO() { CategoryId = product.CategoryId };
                var content = new StringContent(JsonConvert.SerializeObject(product.ProductDTO), Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PostAsync("api/product", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            HttpResponseMessage resCategory = await client.GetAsync("api/category");
            if (resCategory.IsSuccessStatusCode)
            {
                var resultCategory = await resCategory.Content.ReadAsStringAsync();
                product.CategoriesDTO = JsonConvert.DeserializeObject<List<CategoryDTO>>(resultCategory);
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            ProductDTOEdit pdto = new ProductDTOEdit();
            HttpClient client = _productAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync($"api/product/{id}");
            HttpResponseMessage resCategory = await client.GetAsync("api/category");
            if (res.IsSuccessStatusCode & resCategory.IsSuccessStatusCode)
            {
                pdto.ProductDTO = JsonConvert.DeserializeObject<ProductDTO>(await res.Content.ReadAsStringAsync());
                pdto.CategoriesDTO = JsonConvert.DeserializeObject<List<CategoryDTO>>(await resCategory.Content.ReadAsStringAsync());
                pdto.CategoryId = pdto.ProductDTO.ProductCategory.CategoryId;
                
            }

            return View(pdto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoriesDTO,ProductDTO")]ProductDTOEdit product)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                        if (file.Length > 0)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                            Console.WriteLine(fileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                product.ProductDTO.ProductImage = file.FileName;
                            }
                        }
                    }
                }
                HttpClient client = _productAPI.InitializeClient();
                product.ProductDTO.ProductCategory = new CategoryDTO() { CategoryId = product.CategoryId };
                var content = new StringContent(JsonConvert.SerializeObject(product.ProductDTO), Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PutAsync($"api/product/{id}", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(product);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            ProductDTO dto = new ProductDTO();
            HttpClient client = _productAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync($"api/product/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<ProductDTO>(result);
            }
            if (dto == null)
            {
                return NotFound();
            }
            return View(dto);
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