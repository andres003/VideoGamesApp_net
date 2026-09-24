using VideoGamesApp.Application.DTOs;

namespace VideoGamesApp.Application.Interfaces
{
    public interface ICreateOrderUseCase
    {
        Task<OrderResponseDto> ExecuteAsync(CreateOrderDto dto);
    }
}
