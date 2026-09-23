using VideoGamesApp.Domain.Entities;

namespace VideoGamesApp.Domain.Interfaces
{
    public interface IVideoGameRepository
    {
        Task<VideoGame?> GetByIdAsync(Guid id);
        Task<IEnumerable<VideoGame>> GetAllAsync();
        Task AddAsync(VideoGame videoGame);
        Task UpdateAsync(VideoGame videoGame);
        Task DeleteAsync(Guid id);
    }
}
