using System.Security.Claims;
using VMTP.Authorization.Bal.Abstractions.Services.Requests;
using VMTP.Authorization.Bal.Abstractions.Services.Responses;

namespace VMTP.Authorization.Bal.Abstractions.Services;

/// <summary>
/// Сервис занимающийся логикой связанной с токенами
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Создание Токена Доступа
    /// </summary>
    /// <param name="identityInfo">Информация для создания Claims</param>
    /// <param name="lifetime">Продолжительность жизни токена <c>(По стандарту 5 минут)</c></param>
    /// <param name="claims">Claims пользователя</param>
    /// <returns>Токен доступа в виде строки</returns>
    string CreateToken(IdentityInfo? identityInfo = null, TimeSpan lifetime = default, IEnumerable<Claim>? claims = null);
    
    /// <summary>
    /// Создание Refresh Token
    /// </summary>
    /// <returns>Токен в виде строки</returns>
    string CreateRefreshToken();
    
    /// <summary>
    /// Создание пары токенов
    /// </summary>
    /// <param name="identityInfo">Информация для создания Claims</param>
    /// <param name="lifetime">Продолжительность жизни токена <c>(По стандарту 5 минут)</c></param>
    /// <returns>Пару токенов</returns>
    Tokens CreatePair(IdentityInfo? identityInfo, TimeSpan? lifetime = default);
    
    /// <summary>
    /// Получение Claims из существующего токена
    /// </summary>
    /// <remarks>
    /// Валидируется все кроме Lifetime токена
    /// <c>Можно передать как с Bearer так и без него</c>
    /// </remarks>
    /// <param name="token">Токен ввиде строки</param>
    /// <returns>Claims токена</returns>
    ClaimsPrincipal GetPrincipalFromToken(string token);
}