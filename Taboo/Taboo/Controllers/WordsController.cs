using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taboo.DTOs.Word;
using Taboo.Exceptions;
using Taboo.Services.Abstracts;

namespace Taboo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController(IWordService _service) : ControllerBase
    {
        [HttpPost]
        public async Task <IActionResult> Post(WordCreateDTo dto)
        {
            await _service.CreateAsync(dto);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            return Ok(await _service.GetAllAsync());
        }
        [HttpPut]
        public async Task <IActionResult> Put(WordUpdateDto dto,int id)
        {
            try
            {
                await _service.UpdateAsync(dto, id);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is IBaseException ibe)
                {
                    return StatusCode(ibe.StatusCode, new
                    {
                        StatusCode = ibe.StatusCode,
                        Message = ibe.ErrorMessage,
                    });
                }
                else
                {
                    return BadRequest(new
                    {

                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = ex.Message,
                    });
                }
            }

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
