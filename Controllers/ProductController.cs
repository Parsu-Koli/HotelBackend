using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ProductServices services) : ControllerBase
    {
        private readonly ProductServices _services = services;

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _services.GetAllProducts();
            return Ok(result);
        }

        [HttpGet("Id")]
        public IActionResult GetById(int id)
        {
            var result = _services.GetproductById(id);

            if(result == null)
                return NotFound("Enter Valid ID");

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Product product)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            _services.AddProduct(product);

            return  CreatedAtAction(nameof(GetById),
                new { id = product.Id },product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            if(id != product.Id)
                return BadRequest("Enter Valid Id");

            var result = _services.GetproductById(id);

            if (result == null)
                return NotFound("No Product Found");

            _services.UpdateProduct(product);

            var updated = _services.GetproductById(id);

            return Ok(updated);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            var result = _services.GetproductById(id);

            if (result == null)
                return NotFound("Please Enter the valid Id");

            _services.DeleteProduct(id);
            return Ok("Product Deleted");
        }
    }
}
