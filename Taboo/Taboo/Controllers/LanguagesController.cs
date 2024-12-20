using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taboo.DAL;
using Taboo.DTOs.Languages;
using Taboo.Exceptions;
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
          
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(LanguageCreateDTO dto)
        {
            try
            {
                await _service.CreateAsync(dto);
                return Created();
            }
            catch (Exception ex)
            {
                if(ex is IBaseException ibe)
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
        public async Task<IActionResult> Delete(string code)
        {
           await _service.DeleteAsync(code);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update(string code ,LanguageUpdateDTO dto)
        {
            try
            {
               await _service.UpdateAsync(dto,code);
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
        

    }
}
