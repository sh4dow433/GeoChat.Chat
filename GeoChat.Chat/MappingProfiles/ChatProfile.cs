using AutoMapper;
using GeoChat.Chat.Api.Dtos;
using GeoChat.Chat.Core.Models;

namespace GeoChat.Chat.Api.MappingProfiles
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<UserChat, ChatReadDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(uc => uc.Chat.Id))
                .ForMember(dto => dto.Messages, opt => opt.MapFrom(uc => uc.Chat.Messages))
                .ForMember(dto => dto.ChatMembers,
                    opt => opt.MapFrom(uc => uc.Chat.UserChats.Select(uc2 => uc2.User)))
                .ForMember(dto => dto.ChatName, opt => opt.MapFrom(uc => uc.Name))
                .ForMember(dto => dto.LocationId, opt => opt.MapFrom(uc => uc.Chat.LocationId));
        } 
    }

}
