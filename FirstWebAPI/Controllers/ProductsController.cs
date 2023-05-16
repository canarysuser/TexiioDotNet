using FirstWebAPI.Infrastructure;
using FirstWebAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors()]
    /*[Authorization]*/
    public class ProductsController : ControllerBase
    {
        IAsyncRepository<Product, int> _repository; 
        public ProductsController(IAsyncRepository<Product, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }
        [HttpGet(template:"{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            if(productId==0) return BadRequest();   

            var model = await _repository.GetByIdAsync(productId);
            if(model is null)
            {
                return NotFound();
            }
            return Ok(model);
        }
        [HttpPost]
        [Authorization]
        public async Task<IActionResult> CreateProduct(Product model)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            await _repository.UpsertAsync(model);
            return Ok(model);
        }
        [HttpPut(template:"{id}")]
        [Authorization]
        public async Task<IActionResult> UpdateProduct(int id, Product model)
        {
            if(!ModelState.IsValid)
                return BadRequest();    
            var item =await _repository.GetByIdAsync(id);
            if (item is null)
                return NotFound();
            await _repository.UpsertAsync(model);
            return Ok(model);   
        }
        [HttpDelete("{id}")]
        [Authorization]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item is null)
                return NotFound();
            await _repository.RemoveAsync(id);
            return Ok(item);
        }
    }
}
