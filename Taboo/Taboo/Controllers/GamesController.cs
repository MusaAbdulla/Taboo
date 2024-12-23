using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taboo.DTOs.Game;
using Taboo.Services.Abstracts;

namespace Taboo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController(IGameService _service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(GameCreateDto dto)
        {
            await _service.AddAsync(dto);
            return Created();
        }
        [HttpGet]
        public async Task <IActionResult> Get()
        {
            return Ok(await _service.GetAsync());
        }
    }
}
