using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(CategoryServices service) : ControllerBase
    {
        private readonly CategoryServices _service = service;

        // GET: api/category
        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _service.GetCategories();
            return Ok(categories);
        }

        // GET: api/category/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _service.GetCategoryById(id);

            if (category == null)
                return NotFound("Category not found");

            return Ok(category);
        }

        // POST: api/category
        [HttpPost]
        public IActionResult Create([FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.AddCategory(category);

            return CreatedAtAction(nameof(GetById),
                new { id = category.Id },
                category);
        }

        // PUT: api/category/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            if (id != category.Id)
                return BadRequest("ID mismatch");

            var existing = _service.GetCategoryById(id);
            if (existing == null)
                return NotFound("Category not found");

            _service.UpdateCategory(category);

            var result = _service.GetCategoryById(id);

            return Ok(result);
        }

        // DELETE: api/category/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _service.GetCategoryById(id);
            if (existing == null)
                return NotFound("Category not found");

            _service.DeleteCategory(id);

            return Ok("Category Delete");
        }
    }
}
