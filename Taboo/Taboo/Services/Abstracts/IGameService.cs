using Taboo.DTOs.Game;
using Taboo.DTOs.Word;

namespace Taboo.Services.Abstracts
{
    public interface IGameService
    {
        Task<Guid> AddAsync(GameCreateDto dto);
        Task<IEnumerable<GameGetdto>> GetAsync();
        Task<WordForGamedto> StartAsync(Guid id);
        Task<WordForGamedto> PassAsync(Guid id);
        Task<WordForGamedto> SuccessAsync(Guid id);
        Task<WordForGamedto> WrongAsync(Guid id);
        Task EndAsync(Guid id);
    }
}
