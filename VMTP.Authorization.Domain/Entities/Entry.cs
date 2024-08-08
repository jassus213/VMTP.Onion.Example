namespace VMTP.Authorization.Domain.Entities;

public class Entry
{
    public Guid Id { get; init; }
    public Guid AuthenticationId { get; init; }
    public string Ip { get; init; } = string.Empty;
    public string Device { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;

    public Authentication Authentication { get; init; } = null!;
}