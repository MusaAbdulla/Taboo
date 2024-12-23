using Taboo.DTOs.Word;

namespace Taboo.Services.Abstracts
{
    public interface IWordService
    {
        Task CreateAsync(WordCreateDTo dTo);
        Task<IEnumerable<WordGetDto>> GetAllAsync();
        Task DeleteAsync (int id);
        Task UpdateAsync(WordUpdateDto dto,int id);
        Task <WordGetDto> GetbyId(int id);
    }
}
