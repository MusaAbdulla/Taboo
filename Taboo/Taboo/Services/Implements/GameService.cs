using AutoMapper;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Taboo.DAL;
using Taboo.DTOs.Game;
using Taboo.DTOs.Word;
using Taboo.Entities;
using Taboo.External_Services.Abstracts;
using Taboo.Services.Abstracts;

namespace Taboo.Services.Implements
{
    public class GameService(IMapper _mapper ,TabuDbContext _context ,ICacheService _cache) : IGameService
    {
        public async Task<Guid> AddAsync(GameCreateDto dto)
        {
            var entity= _mapper.Map<Game>(dto);
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;   
            
        }

        public Task EndAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GameGetdto>> GetAsync()
        {
            var data = await _context.Games.ToListAsync();
            return _mapper.Map<IEnumerable<GameGetdto>>(data);
        }

        public async Task<WordForGamedto> PassAsync(Guid id)
        {
            var status = await _getGameStatusAsync(id);
            await _addNewWords(status);
            if ( status.Pass< status.MaxPassCount )
            {
                status.Pass++;
                var currentWord = status.Words.Pop();
                await _cache.SetAsync(id.ToString(), status);
                return currentWord;
            }
            return null;
        }

        public async Task<WordForGamedto> StartAsync(Guid id)
        {
           var entity= await _context.Games.FindAsync(id);
            if (entity == null) throw new Exception();
            var words = await _context.Words.Where(x => x.LanguageCode == entity.LanguageCode)
                .Take(10)
                .Select(x => new WordForGamedto
                {
                    Id = x.Id,
                    Name=x.Text,
                    BannedWords=x.BannedWords.Select(y=> y.Text).ToList()

                }).ToListAsync();
            GameStatusDto status = new GameStatusDto()
            {
                Pass = 0,
                Success = 0,
                Words =new Stack<WordForGamedto>(words),
                Wrong=0,
                UserWordIds=words.Select(x=>x.Id).ToList(),
                MaxPassCount=entity.SkipCount,
                LangCode=entity.LanguageCode,

            };
            var word = status.Words.Pop();
            await _cache.SetAsync(id.ToString(), status);
            return word;
        }

        public async Task<WordForGamedto> SuccessAsync(Guid id)
        {
            var status = await _getGameStatusAsync(id);
            await _addNewWords(status);
            status.Success++;
            var word = status.Words.Pop();
            await _cache.SetAsync(id.ToString(),status);
            return word;
        }

        public async  Task<WordForGamedto> WrongAsync(Guid id)
        {
            var status = await _getGameStatusAsync(id);
            await _addNewWords(status);
            status.Wrong++;
            var word = status.Words.Pop();
            await _cache.SetAsync(id.ToString(), status);
            return word;
        }
        async Task<GameStatusDto> _getGameStatusAsync(Guid id)
        {
            GameStatusDto status = await _cache.GetAsync<GameStatusDto>(id.ToString());         
            if  (status == null)
                throw new Exception();
            return ( status);
        }
        async Task _addNewWords(GameStatusDto status)
        {
            if(status.Words.Count<6)
            {
                var newWords=await _context.Words.Where(w => w.LanguageCode == status.LangCode && 
                !status.UserWordIds.Contains(w.Id)).Take(5).Select(x => new WordForGamedto
                {
                    Id = x.Id,
                    Name = x.Text,
                    BannedWords = x.BannedWords.Select(y => y.Text).ToList()

                }).ToListAsync();
                status.UserWordIds.AddRange(newWords.Select(w => w.Id));
                newWords.ForEach(x=> status.Words.Push(x));
            }
        }
    }
}







