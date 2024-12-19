using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taboo.DAL;
using Taboo.DTOs.Languages;
using Taboo.Services.Abstracts;

namespace Taboo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController(ILanguageServices _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           await  _service.GetAllAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(LanguageCreateDTO dto)
        {
            await _service.CreateAsync(dto);
            return Created();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string code)
        {
           await _service.DeleteAsync(code);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update(string code ,LanguageUpdateDTO dto)
        {
            await _service.UpdateAsync(dto,code);
            return Ok();
        }
        

    }
}
