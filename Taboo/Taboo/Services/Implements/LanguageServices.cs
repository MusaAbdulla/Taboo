using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Taboo.DAL;
using Taboo.DTOs.Languages;
using Taboo.Entities;
using Taboo.Exceptions.Language;
using Taboo.Services.Abstracts;

namespace Taboo.Services.Implements
{
    public class LanguageServices(TabuDbContext _context ,IMapper _mapper) : ILanguageServices
    {
        public async Task CreateAsync(LanguageCreateDTO dto)
        {
           
            if (await _context.Languages.AnyAsync(x=> x.Code ==dto.Code))
                throw new LanguageExixstException();
            var data=_mapper.Map<Language>(dto);
            await _context.Languages.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string code)
        {
            var data = await _context.Languages.Where(x => x.Code == code).FirstOrDefaultAsync();
            _context.Languages.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task <IEnumerable<LanguageGettAllDTO>> GetAllAsync()
        {
            var items = await _context.Languages.ToListAsync();
            return  _mapper.Map<IEnumerable<LanguageGettAllDTO>>(items);

        }

        public Task<LanguageGettAllDTO> GetByCode(string code)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(LanguageUpdateDTO dto,string code)
        {
           var data= await _getByCode(code);
            if (data == null) throw new LanguageNotFoundException();
            _mapper.Map(dto,data);
            _context.Languages.Update(data); 
            await _context.SaveChangesAsync();
        }
        async Task<Language?> _getByCode(string code)
            => await _context.Languages.FindAsync(code);
        
        
    }
}
