using MediatR;
using VMTP.Authorization.Application.Models.DTOs;
using VMTP.Authorization.Dal.Abstractions.Contexts;

namespace VMTP.Authorization.Bal.Implementation.Handlers.Commands.Authentication;

public record AddAuthenticationCommand(string Login, string Password) : IRequest<AuthenticationDTO>;

public class AddAuthenticationCommandHandler : IRequestHandler<AddAuthenticationCommand, AuthenticationDTO>
{
    private readonly IAuthenticationWriteContext _context;

    public AddAuthenticationCommandHandler(IAuthenticationWriteContext context)
    {
        _context = context;
    }

    public async Task<AuthenticationDTO> Handle(AddAuthenticationCommand request, CancellationToken cancellationToken)
    {
        var authentication = new Domain.Entities.Authentication()
        {
            Id = Guid.NewGuid(),
            Login = request.Login,
            Password = request.Password
        };

        await _context.Authentications.AddAsync(authentication, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new AuthenticationDTO()
        {
            Id = authentication.Id,
            Login = request.Login,
            Password = request.Password
        };
    }
}