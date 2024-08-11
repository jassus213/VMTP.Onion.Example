namespace VMTP.Authorization.Bal.Abstractions.Managers.Entry.Requests;

public record GetOrCreateEntryAndValidateRequest(Guid AuthenticationId, string Device, string Ip);