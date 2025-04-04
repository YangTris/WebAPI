using Application.Dtos;
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

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] ProductDto productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productService.CreateProductAsync(productDTO);

            return CreatedAtAction(nameof(GetById), new { id = result.ProductId }, result); //return code 201
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(string id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            
            return Ok(product); //return code 200
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProduct()
        {
            var products = await _productService.GetAllProductAsAsync();
            return Ok(products); //return code 200
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productService.DeleteProductAsync(id);
            return NoContent(); //return code 204
        }

        [HttpPut]
        public async Task<IActionResult> UpdateById(string id, [FromBody] UpdateProductDto productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.UpdateProductAsync(id, productDTO);
            
            return NoContent(); //return code 204
        }

    }
}
