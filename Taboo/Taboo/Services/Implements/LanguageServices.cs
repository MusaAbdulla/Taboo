using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Taboo.DAL;
using Taboo.DTOs.Languages;
using Taboo.Entities;
using Taboo.Services.Abstracts;

namespace Taboo.Services.Implements
{
    public class LanguageServices(TabuDbContext _context ,IMapper _mapper) : ILanguageServices
    {
        public async Task CreateAsync(LanguageCreateDTO dto)
        {
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
            if (items is null)
            {
                throw new Exception("Items is null");
            }
            throw new Exception("Success!");

        }
        public async Task UpdateAsync(LanguageUpdateDTO dto,string code)
        {
            var data = _context.Languages.Find(code);
            if (data is null)
            {
                throw new ArgumentNullException();
            }
            var originalCode =data.Code;
            originalCode = dto.Code;
            data.Name = dto.Name;
            data.Icon = dto.IconUrl;
            await _context.SaveChangesAsync();
        }

        
    }
}
