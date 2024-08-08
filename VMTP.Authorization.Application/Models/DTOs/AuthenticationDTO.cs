namespace VMTP.Authorization.Application.Models.DTOs;

public class AuthenticationDTO
{
    public Guid Id { get; init; }
    public string Login { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;

    public ICollection<EntryDTO> Entries { get; init; } = new List<EntryDTO>();
}