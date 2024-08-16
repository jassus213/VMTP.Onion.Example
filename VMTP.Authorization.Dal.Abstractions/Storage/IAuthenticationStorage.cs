using VMTP.Authorization.Application.Models.DTOs;

namespace VMTP.Authorization.Dal.Abstractions.Storage;

public interface IAuthenticationStorage
{
    Task<AuthenticationDTO> AddAsync(string login, string password, CancellationToken cancellationToken);
    
    Task<AuthenticationDTO?> FindByLoginAsync(string login, CancellationToken cancellationToken);
}