using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class OrderController(OrderServices services) : ControllerBase
    {
        private readonly OrderServices _services= services;

        [HttpGet]
        public IActionResult GetAll()
        {
            var result =_services.GetOrders();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int id)
        {
            var result = _services.GetOrderById(id);

            if(result == null)
                return NotFound("Enter the Invalid Id");

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _services.Add(order);

            return CreatedAtAction(nameof(GetById),
                new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Order order)
        {
            if(id != order.Id)
                return BadRequest("Enter the valid Id");

            var result = _services.GetOrderById(id);

            if (result == null)
                return NotFound("Order Not Found");

            _services.Update(order);

            var updated = _services.GetOrderById(id);

            return Ok(updated);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            var result = _services.GetOrderById(id);
            if (result == null)
                return NotFound("Enter Valid Id");

            _services.Delete(id);
            return Ok("Order Deleted");
        }
    }
}
