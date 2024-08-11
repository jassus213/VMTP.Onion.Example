namespace VMTP.Authorization.Bal.Abstractions.Managers.Token.Requests;

/// <summary>
/// Запрос об обновлении пары токенов
/// </summary>
/// <param name="AccessToken">Токен доступа</param>
/// <param name="RefreshToken">Рефреш токен</param>
public record UpdatePairRequest(string AccessToken, string RefreshToken);