using VideoGamesApp.Application.DTOs;
using VideoGamesApp.Application.Interfaces;
using VideoGamesApp.Domain.Interfaces;

namespace VideoGamesApp.Application.UserCases
{
    public class GetAllVideoGamesUseCase : IGetAllVideoGamesUseCase
    {
        private readonly IVideoGameRepository _videoGameRepository;


        public GetAllVideoGamesUseCase(IVideoGameRepository videoGameRepository)
        {
            _videoGameRepository = videoGameRepository;
        }
        public async Task<IEnumerable<VideoGameResponseDTO>> ExecuteAsync()
        {
            var games = await _videoGameRepository.GetAllAsync();

            return games.Select(game => new VideoGameResponseDTO(
                game.Id,
                game.Title,
                game.Platform,
                game.Price,
                game.Stock,
                game.Genre,
                game.IsPreOrder,
                game.ReleaseDate
                ));
        }
    }
}
