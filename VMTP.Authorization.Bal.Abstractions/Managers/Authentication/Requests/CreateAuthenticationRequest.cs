namespace VMTP.Authorization.Bal.Abstractions.Managers.Authentication.Requests;

public record CreateAuthenticationRequest(string Login, string Password, string Ip, string Device);