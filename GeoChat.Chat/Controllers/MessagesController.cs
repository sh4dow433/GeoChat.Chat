using AutoMapper;
using GeoChat.Chat.Api.Dtos;
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
        private readonly IMapper _mapper;

        public MessagesController(IChatService chatService, IMapper mapper)
        {
            _chatService = chatService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Send(MessageCreateDto messageDto)
        {
            var userId = HttpContext.User?.FindFirst("Id")?.Value;
            if (userId == null)
            {
                return Unauthorized(new { ErrorMessage = "No user id claim" });
            }
            if (userId != messageDto.UserId)
            {
                return Unauthorized(new { ErrorMessage = "You cannot add a friend for other user" });
            }
            var message = _mapper.Map<Message>(messageDto);
            await _chatService.SendMessageAsync(message);
            return Ok(new { Message = "Message was sent" });
        }
    }
}
