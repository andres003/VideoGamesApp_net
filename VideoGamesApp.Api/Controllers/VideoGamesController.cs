using Microsoft.AspNetCore.Mvc;
using VideoGamesApp.Application.DTOs;
using VideoGamesApp.Application.Interfaces;
using VideoGamesApp.Domain.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VideoGamesApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGamesController : ControllerBase
    {
        private readonly ICreateVideoGameUseCase _createVideoGameUseCase;
        private readonly IGetAllVideoGamesUseCase _getAllVideoGamesUseCase;

        public VideoGamesController(
            ICreateVideoGameUseCase createVideoGameUseCase,
            IGetAllVideoGamesUseCase getAllVideoGamesUseCase)
        {
            _createVideoGameUseCase = createVideoGameUseCase;
            _getAllVideoGamesUseCase = getAllVideoGamesUseCase;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVideoGameDTO dto)
        {
            try
            {
                var result = await _createVideoGameUseCase.ExecuteAsync(dto);
                return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
            }
            catch (DomainException ex)
            {
                // Atrapamos las reglas de negocio rotas del Dominio y las devolvemos como 400 Bad Request
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllVideoGamesUseCase.ExecuteAsync();
            return Ok(result);
        }
    }
}
