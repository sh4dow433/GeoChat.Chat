using GeoChat.Identity.Api.Dtos;

namespace GeoChat.Chat.Api.Dtos;

public record ChatReadDto
{
    public int Id { get; init; }
    public List<MessageReadDto> Messages { get; init; } = new();
    public List<UserReadDto> ChatMembers { get; set; } = new();
    public string ChatName { get; init; } = null!;
    public int? LocationId { get; set; }
}

