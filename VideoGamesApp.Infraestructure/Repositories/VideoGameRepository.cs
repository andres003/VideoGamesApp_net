using Microsoft.EntityFrameworkCore;
using VideoGamesApp.Domain.Entities;
using VideoGamesApp.Domain.Interfaces;
using VideoGamesApp.Infraestructure.Persistence;

namespace VideoGamesApp.Infraestructure.Repositories
{
    public class VideoGameRepository : IVideoGameRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoGameRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(VideoGame videoGame)
        {
            await _context.VideoGames.AddAsync(videoGame);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var game = await GetByIdAsync(id);
            if (game != null)
            {
                _context.VideoGames.Remove(game);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<VideoGame>> GetAllAsync()
        {
            return await _context.VideoGames.ToListAsync();
        }

        public async Task<VideoGame?> GetByIdAsync(Guid id)
        {
            return await _context.VideoGames.FindAsync(id);
        }

        public async Task UpdateAsync(VideoGame videoGame)
        {
            _context.VideoGames.Update(videoGame);
            await _context.SaveChangesAsync();
        }
    }
}
