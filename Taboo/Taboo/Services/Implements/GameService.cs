using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Taboo.DAL;
using Taboo.DTOs.Game;
using Taboo.Entities;
using Taboo.Services.Abstracts;

namespace Taboo.Services.Implements
{
    public class GameService(IMapper _mapper ,TabuDbContext _context ,IMemoryCache _cache) : IGameService
    {
        public async Task<Guid> AddAsync(GameCreateDto dto)
        {
            var entity= _mapper.Map<Game>(dto);
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;   
            
        }

        public async Task<IEnumerable<GameGetdto>> GetAsync()
        {
            var data = await _context.Games.ToListAsync();
            return _mapper.Map<IEnumerable<GameGetdto>>(data);
        }

        public async Task StartAsync(Guid id)
        {
           var entity= await _context.Games.FindAsync(id);
            if (entity == null) throw new Exception();
            if (entity.Score==null) throw new Exception();
            
        }
    }
}







