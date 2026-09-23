using VideoGamesApp.Application.DTOs;

namespace VideoGamesApp.Application.Interfaces
{
    public interface ICreateVideoGameUseCase
    {
        Task<VideoGameResponseDTO> ExecuteAsync(CreateVideoGameDTO dto);
    }
}
