namespace VMTP.Authorization.Application.Models.Models;

public record AuthenticationDeepModel(Guid AuthenticationId, Guid EntryId, string Email, string Ip, string Device);