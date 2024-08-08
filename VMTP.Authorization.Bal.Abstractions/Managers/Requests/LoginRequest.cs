namespace VMTP.Authorization.Bal.Abstractions.Managers.Requests;

public record LoginRequest(string Login, string Password, string Ip, string Device);