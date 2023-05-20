using System.ComponentModel.DataAnnotations;

namespace GeoChat.Chat.Api.Dtos;

public record MessageCreateDto(
    [Required]
    int ChatId,
    [Required]
    string UserId,
    [Required]
    string Content
);
