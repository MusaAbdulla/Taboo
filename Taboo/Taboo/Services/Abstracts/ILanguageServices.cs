using Taboo.DTOs.Languages;

namespace Taboo.Services.Abstracts
{
    public interface ILanguageServices
    {
        Task <IEnumerable<LanguageGettAllDTO>> GetAllAsync();
        Task CreateAsync (LanguageCreateDTO dto);
        Task UpdateAsync(LanguageUpdateDTO dto,string code);
        Task DeleteAsync( string code);
     
    }
}
