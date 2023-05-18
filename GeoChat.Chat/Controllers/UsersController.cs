using AutoMapper;
using GeoChat.Chat.Core.Repos;
using GeoChat.Chat.Infra.DbAccess;
using GeoChat.Identity.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoChat.Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet("name/{name}")]
        [Authorize]
        public async Task<IActionResult> GetAll(string name)
        {
            var userId = HttpContext.User?.FindFirst("Id")?.Value;
            if (userId == null)
            {
                return Unauthorized(new { ErrorMessage = "No user id claim" });
            }
            var users = await _unitOfWork.UsersRepo.GetUsersByName(name, userId);
            var response = _mapper.Map<IEnumerable<UserReadDto>>(users);
            return Ok(response);
        }

    }
}
