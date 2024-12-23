using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Taboo.DAL;
using Taboo.DTOs.Word;
using Taboo.Entities;
using Taboo.Exceptions.Word;
using Taboo.Services.Abstracts;

namespace Taboo.Services.Implements
{
    public class WordService(TabuDbContext _context,IMapper _mapper) : IWordService
    {
        public async Task CreateAsync(WordCreateDTo dto)
        {
            //if (await _context.Words.AnyAsync(x => x.LanguageCode == dto.Language))
               //throw new Exception();
            if (dto.BannedWords.Count() != 8)
                throw new InvalidBannedWordCountException();
           var data=  _mapper.Map<Word>(dto);
            var BannedWords = dto.BannedWords.Select(x => new BannedWord
            {
                Text=x
            }).ToList();
            data.BannedWords = BannedWords;
            await _context.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
           var data= await _context.Words.Where(x=> x.Id == id).FirstOrDefaultAsync();
            _context.Words.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WordGetDto>> GetAllAsync()
        {
            var data = await _context.Words.Include(x=> x.BannedWords).ToListAsync();
            return _mapper.Map<IEnumerable<WordGetDto>>(data);
        }

        public Task<WordGetDto> GetbyId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(WordUpdateDto dto,int id)
        {
            var data= await _getById(id);
            if (data == null) throw new WordNotFoundException();
            var BannedWords = dto.BannedWords.Select(x => new BannedWord
            {
                Text = x,
            }).ToList();
            _mapper.Map(dto, data);
            _context.Words.Update(data);
            await _context.SaveChangesAsync();
        }
        async Task<Word?> _getById(int id)
            => await _context.Words.FindAsync( id);
    }
}
