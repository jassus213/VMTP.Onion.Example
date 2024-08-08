namespace VMTP.Authorization.Bal.Abstractions.Services.Responses;

/// <summary>
/// Пара токенов
/// </summary>
/// <param name="AccessToken">Access Token</param>
/// <param name="RefreshToken">Refresh Token</param>
public record Tokens(string AccessToken, string RefreshToken);