using MediatR;
using Microsoft.EntityFrameworkCore;
using VMTP.Code.Dal.Abstractions.Contexts;

namespace VMTP.Code.Bal.Implementation.Handlers.Commands;

public record DeleteCodeCommand(Guid Id) : IRequest;

file class DeleteCodeCommandHandler : IRequestHandler<DeleteCodeCommand>
{
    private readonly ICodeWriteContext _context;

    public DeleteCodeCommandHandler(ICodeWriteContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCodeCommand request, CancellationToken cancellationToken)
        => await _context.Codes
            .Where(x => x.Id == request.Id)
            .ExecuteDeleteAsync(cancellationToken);
}