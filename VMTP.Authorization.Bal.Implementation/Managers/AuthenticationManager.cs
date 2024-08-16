using VMTP.Authorization.Application.Exceptions;
using VMTP.Authorization.Application.Models.Models;
using VMTP.Authorization.Bal.Abstractions.Managers.Authentication;
using VMTP.Authorization.Bal.Abstractions.Managers.Authentication.Requests;
using VMTP.Authorization.Bal.Abstractions.Providers.Authentication.Requests;
using VMTP.Authorization.Dal.Abstractions.Storage;
using VMTP.Authorization.Domain.Utilities;

namespace VMTP.Authorization.Bal.Implementation.Managers;

public class AuthenticationManager : IAuthenticationManager
{
    private readonly IAuthenticationStorage _authenticationStorage;
    private readonly IAuthenticationAndEntryUnitOfWork _unitOfWork;

    public AuthenticationManager(IAuthenticationStorage authenticationStorage,
        IAuthenticationAndEntryUnitOfWork unitOfWork)
    {
        _authenticationStorage = authenticationStorage;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthenticationDeepModel> CreateAuthenticationWithEntryAsync(CreateAuthenticationRequest request,
        CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        
        var authentication =
            await _unitOfWork.AuthenticationStorage.AddAsync(request.Login, request.Password, cancellationToken);

        var entry = await _unitOfWork.EntryStorage.AddAsync(authentication.Id, request.Ip, request.Device,
            cancellationToken);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return new AuthenticationDeepModel(authentication.Id, entry.Id, authentication.Login, entry.Ip, entry.Device);
    }

    public async Task<AuthenticationModel?> SearchAuthenticationAsync(string login, CancellationToken cancellationToken)
    {
        var authentication = await _authenticationStorage.FindByLoginAsync(login, cancellationToken);
        return authentication == null ? null : new AuthenticationModel(authentication.Id, authentication.Login);
    }

    public async Task<AuthenticationModel> GetAuthenticationAndValidateAsync(
        GetAuthenticationAndValidateRequest request,
        CancellationToken cancellationToken)
    {
        var authentication = await _authenticationStorage.FindByLoginAsync(request.Login, cancellationToken);

        if (authentication == null)
            throw new UserIsNotRegisteredException();

        if (authentication.Password == HashUtil.ComputeHash(request.Password))
            throw new WrongPasswordException();

        return new AuthenticationModel(authentication.Id, authentication.Login);
    }
}