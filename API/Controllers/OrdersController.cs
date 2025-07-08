using Application.Orders.Commands;
using Application.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderApplicationService _service;

        public OrdersController(IOrderApplicationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            var result = await _service.CreateOrderAsync(command);

            return result.Success
                ? Ok(result)
                : BadRequest(result);
        }
    }
}