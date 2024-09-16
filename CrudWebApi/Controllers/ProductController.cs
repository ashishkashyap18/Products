using CrudWebApi.Interfaces;
using CrudWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productServices.GetProducts();
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _productServices.GetProductById(id);
                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }
                return Ok(product);
            }
            catch (ArgumentException)
            {
                return BadRequest("Invalid product ID.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is required.");
            }
            var createdProduct = await _productServices.CreateProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct?.Id },createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is required.");
            }
            else if (id != product.Id)
            {
                return BadRequest("Product ID mismatch.");
            }
            var success = await _productServices.UpdateProduct(product);
            if (!success)
            {
                return NotFound($"Product with ID {id} not found for update.");
            }
            return Ok("Product updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var success = await _productServices.DeleteProduct(id);
            if (!success)
            {
                return NotFound($"Product with ID {id} not found for deletion.");
            }
            return Ok("Product deleted successfully.");
        }

        [HttpGet("units")]
        public ActionResult<IEnumerable<string>> GetUnits()
        {
            var units = Enum.GetNames(typeof(Unit)).ToList();
            return Ok(units);
        }
    }
}
