using AutoMapper;
using GeoChat.Chat.Api.Dtos;
using GeoChat.Chat.Core.Models;

namespace GeoChat.Chat.Api.MappingProfiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageReadDto>()
                .ForMember(dto => dto.UserName, opt => opt.MapFrom(m => m.User.Name));
        }
    }   

}
