using Microsoft.EntityFrameworkCore;
using VMTP.Dal.Abstractions;

namespace VMTP.Code.Dal.Abstractions.Contexts;

public interface ICodeWriteContext : ISavableContext
{
    DbSet<Domain.Entities.Code> Codes { get; }
}