using GeoChat.Chat.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoChat.Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                //get chat id
                return Ok(id);

            }catch (KeyNotFoundException keyEx)
            {
                return NotFound(keyEx.Message);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok("Chat");

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(Core.Models.Chat chat)
        {
            try
            {
                return Ok(chat);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
