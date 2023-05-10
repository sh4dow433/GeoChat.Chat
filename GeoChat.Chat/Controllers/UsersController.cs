using AutoMapper;
using GeoChat.Chat.Core.Repos;
using GeoChat.Chat.Infra.DbAccess;
using GeoChat.Identity.Api.Dtos;
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


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetAll(string name)
        {
            var users = await _unitOfWork.UsersRepo.GetUsersByName(name);
            var response = _mapper.Map<IEnumerable<UserReadDto>>(users);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Core.Models.User user)
        {
            throw new NotImplementedException();
        }
    }
}
