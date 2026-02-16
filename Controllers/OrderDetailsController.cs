using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController(OrderDetailsServices services) : ControllerBase
    {
        private readonly OrderDetailsServices _services = services;


        [HttpGet]
        public IActionResult GetAll()
        {
            var orderDetails = _services.GetOrderDetails();
            return Ok(orderDetails);
        }

        [HttpGet("{ID}")]
        public IActionResult Get(int id)
        {
            var result = _services.GetOrderDetailsById(id);
            if(result == null) 
                return NotFound("Category not found");

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] OrderDetails orderDetails)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _services.AddOrderDetails(orderDetails);

            return CreatedAtAction(nameof(Get),
                new
                {
                    id = orderDetails.Id
                },
                orderDetails);
        }

        [HttpPost("{id}")]
        public IActionResult Update(int id , [FromBody] OrderDetails orderDetails)
        {
            if(id !=  orderDetails.Id)
                return BadRequest("ID MisMatch");

            var result = _services.GetOrderDetailsById(id);

            if (result == null)
                return NotFound("Order Details Not Found");

            _services.UpdateOrderDetails(orderDetails);

            var updated = _services.GetOrderDetailsById(id);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _services.GetOrderDetailsById(id);

            if (result == null)
                return NotFound("ID Not Found");

            _services.DeleteOrderDetails(id);

            return Ok(result);
        }
    }
}
