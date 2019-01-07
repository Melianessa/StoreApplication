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
    public class CategoryController : Controller
    {
        StoreAPI _categoryAPI = new StoreAPI();

        public async Task<IActionResult> Index()
        {
            List<CategoryDTO> dto = new List<CategoryDTO>();
            HttpClient client = _categoryAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync("api/category");
            if (res.IsSuccessStatusCode)
            {
                var result = await res.Content.ReadAsStringAsync();
                dto = JsonConvert.DeserializeObject<List<CategoryDTO>>(result);
            }

            return View(dto);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CreationDate,CategoryName,CategoryDescription,CategorySortOrder")] CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _categoryAPI.InitializeClient();
                var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8,
                    "application/json");
                HttpResponseMessage res = await client.PostAsync("api/category", content);
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(category);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryDTO dto = new CategoryDTO();
            HttpClient client = _categoryAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync($"api/category/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<CategoryDTO>(result);
            }

            if (dto == null)
            {
                return NotFound();
            }

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id,
            [Bind(
                "CategoryId,CreationDate,CategoryName,CategoryDescription,CategorySortOrder")]
            CategoryDTO category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                HttpClient client = _categoryAPI.InitializeClient();
                var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8,
                    "application/json");
                HttpResponseMessage res = client.PutAsync($"api/category/{id}", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(category);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<CategoryDTO> dto = new List<CategoryDTO>();
            HttpClient client = _categoryAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync("api/category");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<CategoryDTO>>(result);
            }

            var category = dto.SingleOrDefault(p => p.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            HttpClient client = _categoryAPI.InitializeClient();
            HttpResponseMessage res = client.DeleteAsync($"api/category/{id}").Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}