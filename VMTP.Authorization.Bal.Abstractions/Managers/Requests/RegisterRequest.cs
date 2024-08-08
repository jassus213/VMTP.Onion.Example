namespace VMTP.Authorization.Bal.Abstractions.Managers.Requests;

public record RegisterRequest(string Email, string Password, string Device, string Ip);