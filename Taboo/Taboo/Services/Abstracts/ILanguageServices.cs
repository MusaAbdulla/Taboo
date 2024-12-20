using Taboo.DTOs.Languages;

namespace Taboo.Services.Abstracts
{
    public interface ILanguageServices
    {
        Task <IEnumerable<LanguageGettAllDTO>> GetAllAsync();
        Task CreateAsync (LanguageCreateDTO dto);
        Task <LanguageGettAllDTO> GetByCode( string code);
        Task UpdateAsync(LanguageUpdateDTO dto,string code);
        Task DeleteAsync( string code);
     
    }
}
