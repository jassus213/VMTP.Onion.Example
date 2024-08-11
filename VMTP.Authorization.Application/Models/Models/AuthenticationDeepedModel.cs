namespace VMTP.Authorization.Application.Models.Models;

public record AuthenticationDeepedModel(Guid AuthenticationId, Guid EntryId, string Email, string Ip, string Device);