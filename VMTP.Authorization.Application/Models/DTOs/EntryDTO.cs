namespace VMTP.Authorization.Application.Models.DTOs;

public class EntryDTO
{
    public Guid Id { get; init; }
    public Guid AuthenticationId { get; init; }
    public string Ip { get; init; } = string.Empty;
    public string Device { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;

    public AuthenticationDTO Authentication { get; init; } = null!;
}