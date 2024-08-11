namespace VMTP.Authorization.Bal.Abstractions.Providers.Authentication.Requests;

public record GetAuthenticationAndValidateRequest(string Login, string Password);