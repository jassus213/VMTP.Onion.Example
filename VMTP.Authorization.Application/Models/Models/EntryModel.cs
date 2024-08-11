namespace VMTP.Authorization.Application.Models.Models;

public record EntryModel(Guid Id, Guid AuhtorizationId, string Device, string Ip, bool IsTrusted);