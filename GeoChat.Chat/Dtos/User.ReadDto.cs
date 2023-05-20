namespace GeoChat.Identity.Api.Dtos;

public record UserReadDto 
{
    public string Id { get; init; } = null!;
    public string UserName { get; init; } = null!;
} 
