using API.DTOs;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
  
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        private static ProductDTO ProductToDTO(Product product) =>
            new ProductDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
            };

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = new Product
            {
                ProductId = Guid.NewGuid().ToString(),
                Name = productDTO.Name,
                Description = productDTO.Description,
                CategoryId = productDTO.CategoryId,
            };
            var result = await _productService.CreateProductAsync(product);

            return CreatedAtAction(nameof(GetProductById), new { id = result.ProductId }, ProductToDTO(result)); //return code 201
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            
            return Ok(ProductToDTO(product)); //return code 200
        }
    }
}
