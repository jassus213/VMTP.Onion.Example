namespace VMTP.Authorization.Bal.Abstractions.Services.Requests;

/// <summary>
/// Информация необходимая для создания Access токена
/// </summary>
public class IdentityInfo
{
    /// <summary>
    /// Идентификатор авторизации
    /// </summary>
    public required Guid AuthorizationId { get; set; }
    
    /// <summary>
    /// Идентификатор входа
    /// </summary>
    public required Guid EntryId { get; set; }
    
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public required string Login { get; set; }
    
    /// <summary>
    /// IP адрес пользователя
    /// </summary>
    public required string Ip { get; set; }
}