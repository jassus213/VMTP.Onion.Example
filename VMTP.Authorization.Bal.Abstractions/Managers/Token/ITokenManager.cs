using VMTP.Authorization.Bal.Abstractions.Managers.Token.Requests;
using VMTP.Authorization.Bal.Abstractions.Services.Responses;

namespace VMTP.Authorization.Bal.Abstractions.Managers.Token;

/// <summary>
/// Менеджер занимающийся управлением токенами
/// </summary>
public interface ITokenManager
{
    /// <summary>
    /// Метод создающий Refresh токен
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    Task<string> CreateRefreshToken(CancellationToken cancellationToken);

    /// <summary>
    /// Метод создающий токен доступа
    /// </summary>
    /// <param name="request"><see cref="CreateAccessTokenRequest"/></param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    Task<string> CreateAccessToken(CreateAccessTokenRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод создающий пару токенов
    /// </summary>
    /// <param name="request"><see cref="CreatePairRequest"/></param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    Task<Tokens> CreatePair(CreatePairRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод обновляющий пару токенов
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    Task<Tokens> UpdatePairAsync(UpdatePairRequest request, CancellationToken cancellationToken);
}