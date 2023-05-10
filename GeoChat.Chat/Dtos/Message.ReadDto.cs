namespace GeoChat.Chat.Api.Dtos;

public record MessageReadDto 
{
    public int Id { get; init; }
    public int ChatId { get; init; }
    public string UserId { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public string Content { get; init; } = null!;
    public DateTime TimeSent { get; init; }
} 
