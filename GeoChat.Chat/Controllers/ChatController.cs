using AutoMapper;
using GeoChat.Chat.Api.Dtos;
using GeoChat.Chat.Core.Interfaces;
using GeoChat.Chat.Core.Models;
using GeoChat.Chat.Core.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GeoChat.Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IMapper _mapper;

        public ChatController(IChatService chatService, IMapper mapper)
        {
            _chatService = chatService;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var userId = HttpContext.User?.FindFirst("Id")?.Value;
            if (userId == null)
            {
                return Unauthorized(new { ErrorMessage = "No user id claim" });
            }
            var userChats = await _chatService.GetUserChatsForUser(userId);
            var mappedChats = _mapper.Map<IEnumerable<ChatReadDto>>(userChats);
            return Ok(mappedChats);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ChatCreateDto newChat)
        {
            var userId = HttpContext.User?.FindFirst("Id")?.Value;
            if (userId == null)
            {
                return Unauthorized(new { ErrorMessage = "No user id claim" });
            }
            if (userId != newChat.UserId)
            {
                return Unauthorized(new { ErrorMessage = "You cannot add a friend for other user" });
            }
            await _chatService.CreateChatAsync(newChat.UserId, newChat.FriendUserId);
            return Ok(new { Message = "New chat created" });
        }
    }
}
