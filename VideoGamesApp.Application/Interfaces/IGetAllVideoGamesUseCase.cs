using VideoGamesApp.Application.DTOs;

namespace VideoGamesApp.Application.Interfaces
{
    public interface IGetAllVideoGamesUseCase
    {
        Task<IEnumerable<VideoGameResponseDTO>> ExecuteAsync();
    }
}
