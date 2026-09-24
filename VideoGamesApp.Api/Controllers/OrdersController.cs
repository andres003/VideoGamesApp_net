using Microsoft.AspNetCore.Mvc;
using VideoGamesApp.Application.DTOs;
using VideoGamesApp.Application.Interfaces;
using VideoGamesApp.Domain.Exceptions;

namespace VideoGamesApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICreateOrderUseCase _createOrderUseCase;

        public OrdersController(ICreateOrderUseCase createOrderUseCase)
        {
            _createOrderUseCase = createOrderUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            try
            {
                var result = await _createOrderUseCase.ExecuteAsync(dto);
                return Ok(result);
            }
            catch (DomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
