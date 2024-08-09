namespace VMTP.Code.Dal.Abstractions.Contexts;

public interface ICodeReadContext
{
    IQueryable<Domain.Entities.Code> Codes { get; }
}