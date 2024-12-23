using Taboo.DTOs.Game;

namespace Taboo.Services.Abstracts
{
    public interface IGameService
    {
        Task<Guid> AddAsync(GameCreateDto dto);
        Task<IEnumerable<GameGetdto>> GetAsync();
        Task StartAsync(Guid id);
    }
}
