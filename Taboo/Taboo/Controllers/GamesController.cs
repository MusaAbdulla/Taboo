using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taboo.DTOs.Game;
using Taboo.External_Services.Abstracts;
using Taboo.Services.Abstracts;

namespace Taboo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController(IGameService _service,ICacheService _cache) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(GameCreateDto dto)
        {
            await _service.AddAsync(dto);
            return Created();
        }
       
        [HttpPost("Start/{id}")]
        public async Task <IActionResult> Start(Guid id)
        {
          
            return Ok(await _service.StartAsync(id));
        }
        [HttpPost("Next/{id}")]
        public async Task<IActionResult> Success(Guid id)
        {
          
            return Ok(await _service.SuccessAsync(id));
        }
        [HttpGet]
        public async Task <IActionResult> Get()
        {
            return Ok(await _service.GetAsync());
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Get(string key)
        {
           return Ok(await _cache.GetAsync<string>(key));   
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Set(string key,string value)
        {
            await _cache.SetAsync(key, value,30);
            return Ok();
        }
    }
}
