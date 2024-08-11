namespace VMTP.Authorization.Bal.Abstractions.Managers.Token.Requests;

/// <summary>
/// Запрос для создания токена доступа
/// </summary>
/// <param name="AuthorizationId">Идентификатор авторизации</param>
/// <param name="EntryId">Идентификатор входа</param>
/// <param name="Login">Почта пользователя</param>
/// <param name="Ip">IP адрес пользователя</param>
public record CreateAccessTokenRequest(
    Guid AuthorizationId,
    Guid EntryId,
    string Login,
    string Ip);