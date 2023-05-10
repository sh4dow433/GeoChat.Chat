using AutoMapper;
using GeoChat.Chat.Api.Dtos;
using GeoChat.Chat.Core.Models;
using GeoChat.Identity.Api.Dtos;

namespace GeoChat.Chat.Api.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserReadDto>()
            .ForMember(dto => dto.UserName, opt => opt.MapFrom(u => u.Name));
    }
}
