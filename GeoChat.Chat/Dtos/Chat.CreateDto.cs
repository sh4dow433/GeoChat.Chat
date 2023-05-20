using System.ComponentModel.DataAnnotations;

namespace GeoChat.Chat.Api.Dtos;

public record ChatCreateDto(
    [Required]
    string UserId,
    [Required]
    string FriendUserId
);