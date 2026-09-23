using VideoGamesApp.Domain.Enums;

namespace VideoGamesApp.Application.DTOs
{
    public record VideoGameResponseDTO
    (
        Guid Id,
        string Title,
        string Platform,
        decimal Price,
        int Stock,
        VideoGameGenre Genre,
        bool IsPreOrder,
        DateTime? ReleaseDate
    );
}
