using GeoChat.Chat.Core.Interfaces;
using GeoChat.Chat.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GeoChat.Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IChatService _chatService;

        public MessagesController(IChatService chatService)
        {
            _chatService = chatService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            throw new NotImplementedException();
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Send(Message message)
        {
            var userId = HttpContext.User?.FindFirst("Id")?.Value;
            if (userId == null)
            {
                return Unauthorized(new { ErrorMessage = "No user id claim" });
            }
            if (userId != message.UserId)
            {
                return Unauthorized(new { ErrorMessage = "You cannot add a friend for other user" });
            }
            await _chatService.SendMessageAsync(message);
            return Ok(new { Message = "Message was sent" });
        }
    }
}
