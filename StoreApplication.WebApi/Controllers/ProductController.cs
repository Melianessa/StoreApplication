using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StoreApplication.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private IDataRepository<Product, Guid> _iProductRepository;


        public ProductController(IDataRepository<Product, Guid> repoProduct)
        {
            _iProductRepository = repoProduct;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _iProductRepository.ViewAll();
        }
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Product Get(Guid id)
        {
            return _iProductRepository.View(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Product product)
        {
            _iProductRepository.Create(product);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]Product product)
        {
            _iProductRepository.Edit(product.ProductId, product);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public Guid Delete(Guid id)
        {
            return _iProductRepository.Delete(id);
        }
    }
}
