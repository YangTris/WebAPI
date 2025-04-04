using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var category = await _categoryService.GetAllCategory();
            return Ok(category); //return code 200
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound(); //return code 404
            }
            return Ok(category); //return code 200
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _categoryService.CreateCategoryAsync(categoryDto);
    
            return CreatedAtAction(
                nameof(GetById), 
                new { id = result.CategoryId}, 
                result); //return code 201
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(int id, [FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //return code 400
            }

            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound(); //return code 404
            }

            await _categoryService.UpdateCategoryAsync(id, categoryDto);
            return NoContent(); //return code 204
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                NotFound();
            }
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent(); //return code 204
        }
    }
}
