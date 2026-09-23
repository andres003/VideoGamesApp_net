using VideoGamesApp.Application.DTOs;
using VideoGamesApp.Application.Interfaces;
using VideoGamesApp.Domain.Entities;
using VideoGamesApp.Domain.Interfaces;

namespace VideoGamesApp.Application.UserCases
{
    public class CreateVideoGameUseCase : ICreateVideoGameUseCase
    {
        private readonly IVideoGameRepository _videoGameRepository;
        public CreateVideoGameUseCase(IVideoGameRepository videoGameRepository)
        {
            _videoGameRepository = videoGameRepository;
        }


        public async Task<VideoGameResponseDTO> ExecuteAsync(CreateVideoGameDTO dto)
        {
            VideoGame game = new VideoGame(
                dto.Title,
                dto.Platform,
                dto.Price,
                dto.Stock,
                dto.Genre,
                dto.IsPreOrder,
                dto.ReleaseDate
                );

            await _videoGameRepository.AddAsync(game);

            return new VideoGameResponseDTO(
                    game.Id,
                    game.Title,
                    game.Platform,
                    game.Price,
                    game.Stock,
                    game.Genre,
                    game.IsPreOrder,
                    game.ReleaseDate
                );


        }
    }
}
