using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StoreApplication.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private IDataRepository<Category, Guid> _iCategoryRepository;
        public CategoryController(IDataRepository<Category, Guid> repoCategory)
        {
            _iCategoryRepository = repoCategory;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _iCategoryRepository.ViewAll();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Category Get(Guid id)
        {
            return _iCategoryRepository.View(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Category cat)
        {
            _iCategoryRepository.Create(cat);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]Category cat)
        {
            _iCategoryRepository.Edit(id, cat);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public Guid Delete(Guid id)
        {
            return _iCategoryRepository.Delete(id);
        }
    }
}
