namespace VMTP.Authorization.Domain.Entities;

public class Authentication
{
    public Guid Id { get; init; }
    public string Login { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;

    public ICollection<Entry> Entries { get; init; } = new List<Entry>();
}